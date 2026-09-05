/* ==========================================================================
   Shark GS Solutions — site.js
   Vanilla JS: preloader, scroll reveal, counters, tilt cards, typed text,
   service filters, and AJAX-powered contact form.
   ========================================================================== */
(function () {
  "use strict";

  document.addEventListener("DOMContentLoaded", init);

  function init() {
    handlePreloader();
    handleCursorGlow();
    handleScrollProgress();
    handleNavbarScroll();
    handleMobileNav();
    handleScrollSpy();
    handleRevealAnimations();
    handleCounters();
    handleProgressBars();
    handleTiltCards();
    handleTypedText();
    handleServiceFilters();
    handleBackToTop();
    handleContactForm();
  }

  /* ---------------------------------------------------------------------
     Preloader — fade out once the window has finished loading
  --------------------------------------------------------------------- */
  function handlePreloader() {
    var pre = document.getElementById("preloader");
    if (!pre) return;
    var done = function () {
      pre.classList.add("loaded");
      setTimeout(function () { pre.style.display = "none"; }, 700);
    };
    if (document.readyState === "complete") {
      setTimeout(done, 300);
    } else {
      window.addEventListener("load", function () { setTimeout(done, 300); });
      // Safety net in case 'load' is delayed by slow third-party assets
      setTimeout(done, 2200);
    }
  }

  /* ---------------------------------------------------------------------
     Cursor glow that follows the mouse (desktop only)
  --------------------------------------------------------------------- */
  function handleCursorGlow() {
    var glow = document.getElementById("cursorGlow");
    if (!glow || !window.matchMedia("(hover: hover) and (pointer: fine)").matches) return;
    document.addEventListener("mousemove", function (e) {
      glow.classList.add("active");
      glow.style.left = e.clientX + "px";
      glow.style.top = e.clientY + "px";
    });
    document.addEventListener("mouseleave", function () { glow.classList.remove("active"); });
  }

  /* ---------------------------------------------------------------------
     Scroll progress bar across the top of the viewport
  --------------------------------------------------------------------- */
  function handleScrollProgress() {
    var bar = document.getElementById("scrollProgress");
    if (!bar) return;
    window.addEventListener("scroll", function () {
      var scrollTop = window.scrollY;
      var docHeight = document.documentElement.scrollHeight - window.innerHeight;
      var pct = docHeight > 0 ? (scrollTop / docHeight) * 100 : 0;
      bar.style.width = pct + "%";
    }, { passive: true });
  }

  /* ---------------------------------------------------------------------
     Navbar background on scroll
  --------------------------------------------------------------------- */
  function handleNavbarScroll() {
    var nav = document.getElementById("mainNav");
    if (!nav) return;
    var onScroll = function () {
      if (window.scrollY > 40) nav.classList.add("scrolled");
      else nav.classList.remove("scrolled");
    };
    window.addEventListener("scroll", onScroll, { passive: true });
    onScroll();
  }

  /* ---------------------------------------------------------------------
     Mobile nav toggle (custom animated hamburger + Bootstrap collapse)
  --------------------------------------------------------------------- */
  function handleMobileNav() {
    var toggle = document.getElementById("navToggle");
    var menu = document.getElementById("navMenu");
    if (!toggle || !menu) return;

    toggle.addEventListener("click", function () {
      toggle.classList.toggle("open");
    });

    menu.addEventListener("click", function (e) {
      if (e.target.classList.contains("nav-anchor") || e.target.closest(".nav-anchor")) {
        toggle.classList.remove("open");
      }
    });
  }

  /* ---------------------------------------------------------------------
     Scroll-spy: highlight the nav link matching the section in view
  --------------------------------------------------------------------- */
  function handleScrollSpy() {
    var links = document.querySelectorAll(".nav-anchor");
    if (!links.length) return;
    var sections = Array.prototype.map.call(links, function (l) {
      var id = l.getAttribute("href").replace("#", "");
      return document.getElementById(id);
    }).filter(Boolean);

    if (!sections.length || !("IntersectionObserver" in window)) return;

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        var link = document.querySelector('.nav-anchor[href="#' + entry.target.id + '"]');
        if (!link) return;
        if (entry.isIntersecting) {
          links.forEach(function (l) { l.classList.remove("active-link"); });
          link.classList.add("active-link");
        }
      });
    }, { rootMargin: "-45% 0px -50% 0px", threshold: 0 });

    sections.forEach(function (s) { observer.observe(s); });
  }

  /* ---------------------------------------------------------------------
     Generic reveal-on-scroll for .reveal-up / .reveal-left / .reveal-right
  --------------------------------------------------------------------- */
  function handleRevealAnimations() {
    var items = document.querySelectorAll(".reveal-up, .reveal-left, .reveal-right");
    if (!items.length) return;

    if (!("IntersectionObserver" in window)) {
      items.forEach(function (el) { el.classList.add("in-view"); });
      return;
    }

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          var el = entry.target;
          var delay = parseInt(el.getAttribute("data-delay") || "0", 10);
          setTimeout(function () { el.classList.add("in-view"); }, delay);
          observer.unobserve(el);
        }
      });
    }, { threshold: 0.15 });

    items.forEach(function (el) { observer.observe(el); });
  }

  /* ---------------------------------------------------------------------
     Animated number counters (hero stats)
  --------------------------------------------------------------------- */
  function handleCounters() {
    var counters = document.querySelectorAll(".counter");
    if (!counters.length) return;

    var animate = function (el) {
      var target = parseInt(el.getAttribute("data-target"), 10) || 0;
      var duration = 1400;
      var startTime = null;

      function step(ts) {
        if (!startTime) startTime = ts;
        var progress = Math.min((ts - startTime) / duration, 1);
        var eased = 1 - Math.pow(1 - progress, 3); // ease-out-cubic
        el.textContent = Math.floor(eased * target);
        if (progress < 1) requestAnimationFrame(step);
        else el.textContent = target;
      }
      requestAnimationFrame(step);
    };

    if (!("IntersectionObserver" in window)) {
      counters.forEach(animate);
      return;
    }

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          animate(entry.target);
          observer.unobserve(entry.target);
        }
      });
    }, { threshold: 0.6 });

    counters.forEach(function (el) { observer.observe(el); });
  }

  /* ---------------------------------------------------------------------
     Animated progress bars (AI capability strip)
  --------------------------------------------------------------------- */
  function handleProgressBars() {
    var bars = document.querySelectorAll(".progress-fill");
    if (!bars.length || !("IntersectionObserver" in window)) return;

    var observer = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          var el = entry.target;
          var width = el.getAttribute("data-width") || "0";
          requestAnimationFrame(function () { el.style.width = width + "%"; });
          observer.unobserve(el);
        }
      });
    }, { threshold: 0.4 });

    bars.forEach(function (b) { observer.observe(b); });
  }

  /* ---------------------------------------------------------------------
     3D tilt effect on hero / feature images (desktop only)
  --------------------------------------------------------------------- */
  function handleTiltCards() {
    if (!window.matchMedia("(hover: hover) and (pointer: fine)").matches) return;
    var cards = document.querySelectorAll(".tilt-card");
    cards.forEach(function (card) {
      var parent = card.parentElement;
      parent.addEventListener("mousemove", function (e) {
        var rect = card.getBoundingClientRect();
        var x = (e.clientX - rect.left) / rect.width - 0.5;
        var y = (e.clientY - rect.top) / rect.height - 0.5;
        card.style.transform = "perspective(900px) rotateY(" + (x * 10) + "deg) rotateX(" + (-y * 10) + "deg) scale(1.02)";
      });
      parent.addEventListener("mouseleave", function () {
        card.style.transform = "perspective(900px) rotateY(0) rotateX(0) scale(1)";
      });
    });
  }

  /* ---------------------------------------------------------------------
     Typed-text effect cycling through hero keywords
  --------------------------------------------------------------------- */
  function handleTypedText() {
    var el = document.getElementById("typedText");
    if (!el) return;
    var words = ["Digital Excellence", "Intelligent Software", "Secure Cloud Systems", "AI-Powered Growth"];
    var wordIndex = 0, charIndex = 0, deleting = false;

    function tick() {
      var current = words[wordIndex];
      if (!deleting) {
        charIndex++;
        el.textContent = current.substring(0, charIndex);
        if (charIndex === current.length) {
          deleting = true;
          setTimeout(tick, 1600);
          return;
        }
      } else {
        charIndex--;
        el.textContent = current.substring(0, charIndex);
        if (charIndex === 0) {
          deleting = false;
          wordIndex = (wordIndex + 1) % words.length;
        }
      }
      setTimeout(tick, deleting ? 40 : 75);
    }
    tick();
  }

  /* ---------------------------------------------------------------------
     Service card filter chips (Build / Secure & Operate / Grow & Scale)
  --------------------------------------------------------------------- */
  function handleServiceFilters() {
    var chips = document.querySelectorAll(".filter-chip");
    var cols = document.querySelectorAll(".service-col");
    if (!chips.length || !cols.length) return;

    function applyFilter(filter) {
      cols.forEach(function (col) {
        var match = filter === "all" || col.getAttribute("data-category") === filter;
        col.classList.toggle("show", match);
      });
    }

    chips.forEach(function (chip) {
      chip.addEventListener("click", function () {
        chips.forEach(function (c) { c.classList.remove("active"); });
        chip.classList.add("active");
        applyFilter(chip.getAttribute("data-filter"));
      });
    });

    applyFilter("all");
  }

  /* ---------------------------------------------------------------------
     Back-to-top floating button
  --------------------------------------------------------------------- */
  function handleBackToTop() {
    var btn = document.getElementById("backToTop");
    if (!btn) return;
    window.addEventListener("scroll", function () {
      btn.classList.toggle("show", window.scrollY > 500);
    }, { passive: true });
    btn.addEventListener("click", function () {
      window.scrollTo({ top: 0, behavior: "smooth" });
    });
  }

  /* ---------------------------------------------------------------------
     Contact form — client-side validation + AJAX POST to HomeController
  --------------------------------------------------------------------- */
  function handleContactForm() {
    var form = document.getElementById("contactForm");
    if (!form) return;

    var submitBtn = document.getElementById("contactSubmit");
    var btnText = submitBtn.querySelector(".btn-text");
    var btnSpinner = submitBtn.querySelector(".btn-spinner");

    form.addEventListener("submit", function (e) {
      e.preventDefault();
      clearErrors(form);

      setLoading(true);

      var formData = new FormData(form);
      var token = form.querySelector('input[name="__RequestVerificationToken"]');

      fetch("/Home/SubmitContact", {
        method: "POST",
        headers: token ? { "RequestVerificationToken": token.value } : {},
        body: formData
      })
        .then(function (res) { return res.json(); })
        .then(function (data) {
          setLoading(false);
          if (data.success) {
            showToast(data.message || "Thanks! We'll be in touch shortly.", "success");
            form.reset();
            form.querySelectorAll(".is-valid").forEach(function (el) { el.classList.remove("is-valid"); });
          } else {
            applyServerErrors(form, data.errors || {});
            showToast("Please check the highlighted fields and try again.", "danger");
          }
        })
        .catch(function () {
          setLoading(false);
          showToast("Something went wrong sending your message. Please try again.", "danger");
        });
    });

    // Lightweight inline validation as the user types
    form.querySelectorAll(".form-control, .form-select").forEach(function (input) {
      input.addEventListener("input", function () { validateField(input); });
      input.addEventListener("blur", function () { validateField(input); });
    });

    function validateField(input) {
      var valid = input.checkValidity();
      input.classList.toggle("is-invalid", !valid);
      input.classList.toggle("is-valid", valid && input.value.trim() !== "");
      var errorEl = form.querySelector('.field-error[data-for="' + input.id + '"]');
      if (errorEl) errorEl.textContent = valid ? "" : (input.validationMessage || "This field is required.");
    }

    function clearErrors(form) {
      form.querySelectorAll(".field-error").forEach(function (el) { el.textContent = ""; });
      form.querySelectorAll(".is-invalid").forEach(function (el) { el.classList.remove("is-invalid"); });
    }

    function applyServerErrors(form, errors) {
      Object.keys(errors).forEach(function (key) {
        var input = form.querySelector('[name="' + key + '"]');
        var errorEl = form.querySelector('.field-error[data-for="' + key + '"]');
        if (input) input.classList.add("is-invalid");
        if (errorEl) errorEl.textContent = errors[key][0];
      });
    }

    function setLoading(isLoading) {
      submitBtn.disabled = isLoading;
      btnText.classList.toggle("d-none", isLoading);
      btnSpinner.classList.toggle("d-none", !isLoading);
    }

    function showToast(message, type) {
      var toastEl = document.getElementById("feedbackToast");
      var toastBody = document.getElementById("feedbackToastBody");
      toastBody.textContent = message;
      toastEl.classList.remove("text-bg-success", "text-bg-danger");
      toastEl.classList.add(type === "success" ? "text-bg-success" : "text-bg-danger");
      var toast = bootstrap.Toast.getOrCreateInstance(toastEl, { delay: 5000 });
      toast.show();
    }
  }
})();
