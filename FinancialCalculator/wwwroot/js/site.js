// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("form.js-normalize-decimals").forEach(form => {
        const inputs = form.querySelectorAll(".decimal-input");

        const normalize = (el) => {
            if (!el || !el.value) return;
            el.value = el.value.replace(/\s+/g, "").trim();
        };

        inputs.forEach(i => {
            i.addEventListener("blur", () => normalize(i));
            i.addEventListener("change", () => normalize(i));
        });

        form.addEventListener("submit", () => inputs.forEach(i => normalize(i)));
    });
});
