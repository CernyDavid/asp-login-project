// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
[...document.querySelectorAll("img")].forEach(img => {
    if (!img.hasAttribute("src") || img.src === null) {
        console.log("Image without src attribute found. Setting src to 1x1 transparent gif.");
        img.src = 'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7';
    }
})