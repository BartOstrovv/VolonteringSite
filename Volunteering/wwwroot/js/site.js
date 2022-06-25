// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var slideIndex = 1;
var group = 0;
showSlides(slideIndex);
// Next/previous controls
function plusSlides(n, indx) {
    group = indx;
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n, indx) {
    group = indx;
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides " + group.toString());
    var dots = document.getElementsByClassName("demo " + group.toString());

    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
    captionText.innerHTML = dots[slideIndex - 1].alt;
}