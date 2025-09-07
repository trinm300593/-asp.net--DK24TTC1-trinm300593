// Bubble Tea & Coffee Shop - Enhanced JavaScript

// Smooth scrolling for anchor links
document.addEventListener('DOMContentLoaded', function() {
    // Add fade-in animation to elements
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('fade-in');
            }
        });
    }, observerOptions);

    // Observe all product cards and sections
    document.querySelectorAll('.product-card, .card, .hero-section, .fade-in').forEach(el => {
        observer.observe(el);
    });

    // Create scroll indicator
    const scrollIndicator = document.createElement('div');
    scrollIndicator.className = 'scroll-indicator';
    document.body.appendChild(scrollIndicator);

    // Navbar scroll effect and scroll indicator
    window.addEventListener('scroll', function() {
        const navbar = document.querySelector('.navbar');
        const scrollIndicator = document.querySelector('.scroll-indicator');

        // Navbar effect
        if (window.scrollY > 50) {
            navbar.style.background = 'linear-gradient(135deg, rgba(102, 126, 234, 0.95) 0%, rgba(118, 75, 162, 0.95) 100%)';
            navbar.style.backdropFilter = 'blur(10px)';
        } else {
            navbar.style.background = 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)';
            navbar.style.backdropFilter = 'none';
        }

        // Scroll indicator
        const scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
        const scrollHeight = document.documentElement.scrollHeight - document.documentElement.clientHeight;
        const scrollPercent = (scrollTop / scrollHeight) * 100;
        scrollIndicator.style.width = scrollPercent + '%';
    });

    // Loading animation for buttons
    document.querySelectorAll('button[type="submit"]').forEach(button => {
        button.addEventListener('click', function() {
            if (this.form && this.form.checkValidity()) {
                this.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Đang xử lý...';
                this.disabled = true;
            }
        });
    });

    // Product image hover effect
    document.querySelectorAll('.product-card').forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-8px) scale(1.02)';
        });

        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });

    // Cart badge animation
    function animateCartBadge() {
        const badge = document.getElementById('cart-badge');
        if (badge) {
            badge.style.transform = 'scale(1.3)';
            setTimeout(() => {
                badge.style.transform = 'scale(1)';
            }, 200);
        }
    }

    // Search functionality enhancement
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        let searchTimeout;
        searchInput.addEventListener('input', function() {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => {
                performSearch(this.value);
            }, 300);
        });
    }

    function performSearch(searchTerm) {
        const products = document.querySelectorAll('.product-item');
        const searchTermLower = searchTerm.toLowerCase();

        products.forEach(product => {
            const productName = product.getAttribute('data-name');
            const productCategory = product.getAttribute('data-category');

            if (productName.includes(searchTermLower) ||
                (productCategory && productCategory.toLowerCase().includes(searchTermLower))) {
                product.style.display = 'block';
                product.style.animation = 'fadeIn 0.3s ease-out';
            } else {
                product.style.display = 'none';
            }
        });
    }

    // Quantity input enhancement
    document.querySelectorAll('input[type="number"]').forEach(input => {
        input.addEventListener('change', function() {
            if (this.value < 1) this.value = 1;
            if (this.value > 99) this.value = 99;
        });
    });

    // Toast notifications
    function showToast(message, type = 'success') {
        const toast = document.createElement('div');
        toast.className = `toast align-items-center text-white bg-${type} border-0`;
        toast.setAttribute('role', 'alert');
        toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    <i class="fas fa-${type === 'success' ? 'check-circle' : 'exclamation-triangle'} me-2"></i>
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        `;

        const toastContainer = document.getElementById('toast-container') || createToastContainer();
        toastContainer.appendChild(toast);

        const bsToast = new bootstrap.Toast(toast);
        bsToast.show();

        setTimeout(() => {
            toast.remove();
        }, 5000);
    }

    function createToastContainer() {
        const container = document.createElement('div');
        container.id = 'toast-container';
        container.className = 'toast-container position-fixed top-0 end-0 p-3';
        container.style.zIndex = '9999';
        document.body.appendChild(container);
        return container;
    }

    // Add page transition effect
    document.body.classList.add('page-transition');
    setTimeout(() => {
        document.body.classList.add('loaded');
    }, 100);

    // Add animation classes to elements
    const animateElements = () => {
        // Staggered animation for product cards
        const productCards = document.querySelectorAll('.product-card');
        productCards.forEach((card, index) => {
            setTimeout(() => {
                card.classList.add('bounce-in');
            }, 100 * index);
        });

        // Alternate slide-in animations for sections
        const sections = document.querySelectorAll('section, .container > .row');
        sections.forEach((section, index) => {
            if (index % 2 === 0) {
                section.classList.add('slide-in-left');
            } else {
                section.classList.add('slide-in-right');
            }
        });
    };

    // Run animations after page load
    window.addEventListener('load', animateElements);

    // Add hover effects to cards
    document.querySelectorAll('.card').forEach(card => {
        card.classList.add('card-hover-effect');
    });

    // Make functions globally available
    window.showToast = showToast;
    window.animateCartBadge = animateCartBadge;
});
