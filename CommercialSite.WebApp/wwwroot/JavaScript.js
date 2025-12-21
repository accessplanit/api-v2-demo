window.initializeSidebarCollapse = () => {
    const sidebarCollapseButton = document.querySelector('#sidebar-collapse-button');

    if (sidebarCollapseButton) {
        sidebarCollapseButton.addEventListener('click', () => {
            document.querySelector('#sidebar').classList.toggle("expand");
        });
    }
};