// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function openPopover(id) {
    $('.button-remove-item').click(function () {
        $('.button-remove-item').not(this).popover('hide'); //all but this
    });
    $('[data-toggle="popover-remove-item-' + id + '"]').popover(
        {

            container: 'body',
            html: true,
            title: 'Ta bort vara',
            sanitize: false,
            content: function () {
                return $('#hidden-popover-' + id).html()
            }
        }).popover('toggle')
}

function closePopover(id) {
    $('[data-toggle="popover-remove-item-' + id + '"]').popover('hide')
}


function openPopover_Dropdown(id) {
    $('.button-remove-item').click(function () {
        $('.button-remove-item').not(this).popover('hide'); //all but this
    });
    $('[data-toggle="popover-remove-item-dropdown-' + id + '"]').popover(
        {

            container: 'body',
            html: true,
            title: 'Ta bort vara',
            sanitize: false,
            content: function () {
                return $('#hidden-popover-dropdown' + id).html()
            }
        }).popover('toggle')
}

function closePopover_dropdown(id) {
    $('[data-toggle="popover-remove-item-dropdown-' + id + '"]').popover('hide')
}