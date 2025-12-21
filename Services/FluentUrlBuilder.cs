using Constants;
using Services.Models;
using System.Text;

namespace Services
{
    public class FluentUrlBuilder
    {
        private readonly StringBuilder _urlBuilder;

        public FluentUrlBuilder(string baseUrl)
        {
            _urlBuilder = new StringBuilder(baseUrl);
        }

        public FluentUrlBuilder AddPath(string? path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (!_urlBuilder.ToString().EndsWith("/"))
                    _urlBuilder.Append("/");
                _urlBuilder.Append(path);
            }
            return this;
        }

        /// <summary>
        /// Adds query parameters to the URL being built, this can be used for select and filter query with a single property name value
        /// </summary>
        /// <param name="propertyName">
        /// The names of the propertyto include in a filter expression.
        /// </param>
        /// <param name="query">
        /// The filter type i.e. Select, Filter, OrderBy... to apply to the query.
        /// </param>
        /// <param name="filterExpression">
        /// The filter expression specifying the logical or comparison operation (e.g., eq, ne, gt, lt, startswith, contains) (optional).
        /// </param>
        /// <param name="filterValue">
        /// The value to filter by in the query (optional).
        /// </param>
        /// </param>
        /// <returns>
        /// The current instance of <see cref="FluentUrlBuilder"/> with the added query parameters.
        /// </returns>
        public FluentUrlBuilder AddQuery(QueryOperations query, string propertyName, FilterExpressions? filterExpression = null, string? filterValue = null)
        {
            try
            {
                // Validate input parameters
                ValidateRequestParameters(
                    propertyName: propertyName,
                    query: query,
                    filterExpression: filterExpression,
                    filterValue: filterValue
                );

                // Initialize a query parameters list
                var queryParams = new List<string>();
                string queryString = query.ToQueryString();

                switch (query)
                {
                    case QueryOperations.Select:

                        if (propertyName is not null && propertyName.Any())
                            queryParams.Add($"{queryString}={propertyName}");

                        break;

                    case QueryOperations.Filter:

                        if (filterExpression is not null && !string.IsNullOrEmpty(filterValue))
                        {
                            string filterQuery = $"{queryString}=";

                            filterQuery += AddFilterClause(propertyName, (FilterExpressions)filterExpression, filterValue);

                            queryParams.Add(filterQuery);
                        }

                        break;

                    case QueryOperations.OrderBy:
                        if (propertyName is not null && propertyName.Any())
                            queryParams.Add($"{queryString}={propertyName}");
                        break;
                }

                // Append query parameters to the URL
                if (queryParams.Any())
                {
                    if (!_urlBuilder.ToString().Contains("?"))
                        _urlBuilder.Append("?");
                    else
                        _urlBuilder.Append("&");

                    _urlBuilder.Append(string.Join("&", queryParams));
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return this;
        }

        /// <summary>
        /// Adds query parameters to the URL being built, this only Supports multiple property names for the Select query
        /// </summary>
        public FluentUrlBuilder AddQuery(QueryOperations query, IEnumerable<Filter>? filters = null, IEnumerable<string>? propertyNames = null)
        {
            if (query == QueryOperations.OrderBy)
                throw new Exception("This method only supports Select or Filter QueryOperations");

            try
            {
                if(query == QueryOperations.Select)
                    ValidateRequestParameters(
                        query: query,
                        propertyNames: propertyNames
                    );
                if(query == QueryOperations.Filter)
                    ValidateRequestParameters(
                        query: query,
                        filters: filters
                    );

                // Initialize a query parameters list
                var queryParams = new List<string>();
                string queryString = query.ToQueryString();

                switch (query)
                {
                    case QueryOperations.Select:
                        // Handle $select
                        if (propertyNames is not null && propertyNames.Any())
                        {
                            string selectQuery = $"{queryString}={string.Join(",", propertyNames)}";
                            queryParams.Add(selectQuery);
                        }
                        break;

                    case QueryOperations.Filter:

                        List<string> filterQueries = new List<string>();

                        for (int i = 0; i < filters.Count(); i++)
                        {

                            var filter = filters.ElementAt(i);

                            string filterQuery = "";

                            if (i == 0)
                                filterQuery = $"{queryString}=";
                            else
                                filterQuery = $"and ";

                            filterQuery += AddFilterClause(filter.PropertyName, filter.FilterExpression, filter.FilterValue);

                            filterQueries.Add(filterQuery);
                        }

                        queryParams.Add(string.Join("", filterQueries));

                        break;
                }

                // Append query parameters to the URL
                if (queryParams.Any())
                {
                    if (!_urlBuilder.ToString().Contains("?"))
                        _urlBuilder.Append("?");
                    else
                        _urlBuilder.Append("&");

                    _urlBuilder.Append(string.Join("&", queryParams));
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return this;
        }

        /// <summary>
        /// Adds pagination and results sorting query parameters to the URL being built
        /// </summary>
        public FluentUrlBuilder AddPagination(PaginationOptions paginationOptions, int? value = null)
        {
            try
            {
                // Validate input parameters
                ValidateRequestParameters(
                    paginationOptions: paginationOptions,
                    value: value
                );

                // Initialize a query parameters list
                var queryParams = new List<string>();
                string queryString = paginationOptions.ToPaginationString();

                switch (paginationOptions)
                {
                    case PaginationOptions.Top:

                        if (value is not null && value.HasValue)
                            queryParams.Add($"{queryString}={value}");

                        break;

                    case PaginationOptions.Skip:

                        if (value is not null && value.HasValue)
                            queryParams.Add($"{queryString}={value}");

                        break;

                    case PaginationOptions.Count:
                        queryParams.Add($"{queryString}=true");
                        break;
                }


                // Append query parameters to the URL
                if (queryParams.Any())
                {
                    if (!_urlBuilder.ToString().Contains("?"))
                        _urlBuilder.Append("?");
                    else
                        _urlBuilder.Append("&");

                    _urlBuilder.Append(string.Join("&", queryParams));
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return this;
        }

        /// <summary>
        /// Validates parameters passed in, depending on which parameters its evaluating it will validate for the circumstance it is being used for. 
        /// I.e. if the query is filter it will validate the filter parameters.
        /// To see what parameters are required for each query operation see the AddQueryMethod parameters/>
        /// </summary>
        public void ValidateRequestParameters(string? propertyName = null, IEnumerable<string>? propertyNames = null, QueryOperations? query = null, IEnumerable<Filter>? filters = null, PaginationOptions? paginationOptions = null, FilterExpressions? filterExpression = null, string? filterValue = null, int? value = null)
        {
            if (query == QueryOperations.Select)
            {
                // if both propertyName and propertyNames are not null or empty then throw an exception
                if (!string.IsNullOrEmpty(propertyName) && propertyNames != null && propertyNames.Any())
                    throw new ArgumentException("Cannot use both propertyName and propertyNames in a $select query.");

                // if both propertyName and propertyNames are null or empty then throw an exception
                if(string.IsNullOrEmpty(propertyName) && (propertyNames is null || !propertyNames.Any()))
                    throw new ArgumentException("propertyName or propertyNames must be provided for $select query.");
            }

            // Validate filter
            if (query == QueryOperations.Filter)
                if (filters is null)
                {
                    if (propertyName is null)
                        throw new ArgumentNullException(nameof(propertyName), "Property name cannot be null or empty for $filter.");
                    if (filterExpression is null)
                        throw new ArgumentNullException(nameof(filterExpression), "Filter expression cannot be null for $filter.");
                    if (string.IsNullOrEmpty(filterValue))
                        throw new ArgumentNullException(nameof(filterValue), "Filter value cannot be null or empty for $filter.");
                }

            // Validate orderby
            if (query == QueryOperations.OrderBy && string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "Order by field cannot be null or empty for $orderby.");

            // Validate top
            if (paginationOptions == PaginationOptions.Top && value is null)
                throw new ArgumentOutOfRangeException(nameof(value), "value must be -1 or a non-negative integer.");

            // Validate skip
            if (paginationOptions == PaginationOptions.Skip && value is null)
                throw new ArgumentOutOfRangeException(nameof(value), "value must be a non-negative integer.");
        }

        public string AddFilterClause(string propertyName, FilterExpressions filterExpression, string filterValue)
        {

            string filterQuery = "";
            string filterExpressionString = filterExpression.ToFilterExpressionString();

            switch (filterExpression)
            {
                case FilterExpressions.Equals:
                    filterQuery += $"{propertyName} {filterExpressionString} '{filterValue}'";
                    break;
                case FilterExpressions.NotEqual:
                    filterQuery += $"{propertyName} {filterExpressionString} '{filterValue}'";
                    break;
                case FilterExpressions.LessThan:
                    filterQuery += $"{propertyName} {filterExpressionString} '{double.Parse(filterValue)}'";
                    break;
                case FilterExpressions.LessThanOrEqual:
                    filterQuery += $"{propertyName} {filterExpressionString} '{double.Parse(filterValue)}'";
                    break;
                case FilterExpressions.GreaterThan:
                    filterQuery += $"{propertyName} {filterExpressionString} '{double.Parse(filterValue)}'";
                    break;
                case FilterExpressions.GreaterThanOrEqual:
                    filterQuery += $"{propertyName} {filterExpressionString} '{double.Parse(filterValue)}'";
                    break;
                case FilterExpressions.Contains:
                    filterQuery += $"{filterExpressionString}({propertyName}, '{filterValue}')";
                    break;
                case FilterExpressions.StartsWith:
                    filterQuery += $"{filterExpressionString }({propertyName}, '{filterValue}')";
                    break;
                case FilterExpressions.EndsWith:
                    filterQuery += $"{filterExpressionString} ({propertyName}, '{filterValue}')";
                    break;
            }

            return filterQuery;
        }

        /// <summary>
        /// This was mainly put here to have a place to add future developement to this query if needed.
        /// It also helps to keep transparancy of what a request is doing.
        /// </summary>
        public FluentUrlBuilder GetAll() =>
            this;

        /// <summary>
        /// Used at the end of url construction to return the string.
        /// </summary>
        public string ToUrlString() => _urlBuilder.ToString();
    }

}

