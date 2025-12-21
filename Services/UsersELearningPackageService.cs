using System;
using Models;

namespace Services;

public class ELearningPackageService
{
    public UsersELearningPackage? CurrentUsersElearningPackage { get; private set; }

    public void SetUsersELearningPackage(UsersELearningPackage usersElearningPackage)
        => CurrentUsersElearningPackage = usersElearningPackage;

    public UsersELearningPackage? GetUsersELearningPackage()
        => CurrentUsersElearningPackage;
}
