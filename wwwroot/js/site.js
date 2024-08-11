// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $('#data-table').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
    });


    setTimeout(function () {
        $(".alert").fadeOut("slow", function () {
            $(this).alert('close');
        })

    }, 4000);
});

