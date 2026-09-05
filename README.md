# Shark GS Solutions — Company Website (ASP.NET Core MVC)

A modern, animated single-page marketing site generated from the Shark GS
Solutions company profile. Built with **ASP.NET Core MVC (.NET 8)**, Razor
views, and vanilla **JavaScript** (no SPA framework needed).

## ✨ What's inside

- **Hero section** with a typed-text headline, animated counters, floating
  glass cards, and a mouse-tilt hero image.
- **About**, **AI/ML feature strip** (with animated skill bars), **Services**
  grid (19 service lines pulled straight from the company profile, filterable
  by Build / Secure & Operate / Grow & Scale), **Industries**, **Why Choose
  Us**, and a **Contact** section with a validated AJAX form.
- Scroll-triggered reveal animations, a scroll progress bar, cursor glow,
  sticky/blurring navbar with scroll-spy, animated hamburger menu, and a
  back-to-top button.
- Fully data-driven: all services/industries/stats live in
  `Controllers/HomeController.cs` and are rendered via a strongly-typed
  `HomeViewModel`, not hard-coded HTML.
- Contact form posts to `HomeController.SubmitContact` with server-side
  `DataAnnotations` validation and returns JSON consumed by `site.js`.

## 🗂 Project structure

```
SharkGSSolutions/
├── Controllers/HomeController.cs   # Page + contact-form logic, service/industry data
├── Models/                         # ServiceItem, IndustryItem, StatItem, ContactViewModel, HomeViewModel
├── Views/
│   ├── Home/Index.cshtml           # The entire one-page site (sections)
│   ├── Home/Error.cshtml
│   └── Shared/_Layout.cshtml        # Navbar, footer, preloader, toast
├── wwwroot/
│   ├── css/site.css                # Theme, glassmorphism, all animations
│   ├── js/site.js                  # Reveal/counter/tilt/typed-text/AJAX form logic
│   └── images/                     # Photos extracted from the original company profile
├── Program.cs
└── appsettings.json
```

## ▶️ Run it locally

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
cd SharkGSSolutions
dotnet restore
dotnet run
```

Then open the URL printed in the console (typically `https://localhost:5001`
or `http://localhost:5000`).

> Everything renders from CDNs (Bootstrap 5, Bootstrap Icons, Google Fonts),
> so an internet connection is needed the first time you load the page.

## 🎨 Customizing

- **Colors / theme:** edit the CSS custom properties at the top of
  `wwwroot/css/site.css` (`--accent-primary`, `--accent-secondary`, etc.).
- **Services / industries / stats:** edit the private `Get...()` methods in
  `Controllers/HomeController.cs` — the view will update automatically since
  everything is looped from the model.
- **Contact form destination:** wire up a real email/CRM call inside
  `HomeController.SubmitContact` where the `// In production...` comment is.
- **Images:** replace `wwwroot/images/hero-team.jpg` and `ai-collage.jpg`
  with your own photography for production use.

## 🔒 Notes

- The contact form is protected with an anti-forgery token
  (`@Html.AntiForgeryToken()` + `[ValidateAntiForgeryToken]`).
- All animations are dependency-free (no jQuery, no animation library) —
  just `IntersectionObserver`, CSS transitions, and `requestAnimationFrame`.
