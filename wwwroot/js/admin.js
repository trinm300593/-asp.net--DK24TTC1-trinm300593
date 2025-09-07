// Admin Panel JavaScript

document.addEventListener('DOMContentLoaded', function() {
    // Toggle sidebar
    const sidebarToggle = document.querySelector('.sidebar-toggle');
    const adminWrapper = document.querySelector('.admin-wrapper');
    
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function() {
            document.querySelector('.admin-sidebar').classList.toggle('collapsed');
            document.querySelector('.admin-main').classList.toggle('expanded');
        });
    }
    
    // Highlight active menu item
    const currentLocation = window.location.pathname;
    const menuLinks = document.querySelectorAll('.menu-link');
    
    menuLinks.forEach(link => {
        if (link.getAttribute('href') === currentLocation) {
            link.classList.add('active');
        }
    });
    
    // Responsive sidebar
    function checkWidth() {
        if (window.innerWidth < 768) {
            document.querySelector('.admin-sidebar').classList.add('collapsed');
            document.querySelector('.admin-main').classList.add('expanded');
        } else {
            document.querySelector('.admin-sidebar').classList.remove('collapsed');
            document.querySelector('.admin-main').classList.remove('expanded');
        }
    }
    
    // Initial check
    checkWidth();
    
    // Check on resize
    window.addEventListener('resize', checkWidth);
});
