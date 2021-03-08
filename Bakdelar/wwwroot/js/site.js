// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var popoverOpen = 'false';

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
    $('.button-remove-item-dropdown').click(function () {
        $('.button-remove-item-dropdown').not(this).popover('hide'); //all but this
    });

    $('[data-toggle="popover-remove-item-dropdown-' + id + '"]').popover(
        {

            container: 'body',
            html: true,
            title: 'Ta bort vara',
            trigger: 'focus',
            sanitize: false,
            content: function () {
                return $('#hidden-popover-dropdown-' + id).html()
            }
        }).popover('toggle');



    thisElement = $('.hidden-popover-menu');
    thisElement.on('click', function (e2) {
        console.log('triggered');
        console.log('click target=' + e2.target);
        let clickTarget = e2.target;
        $('#keep-open').on('hide.bs.dropdown', function (e) {
            if (clickTarget == 'javascript:void(0)') {
                e.preventDefault();

                console.log('stopped default!')
                clickTarget = "not void(0) :)"
                console.log(clickTarget);
            }
            else {
                console.log('default executed!')
            }
        });

    });
}

function closePopover_Dropdown(id) {
    let thisElement = $('[data-toggle="popover-remove-item-dropdown-' + id + '"]');
    thisElement.popover('hide');


}
$(document).ready(function () {
    $('#dropleft-basket span').click(function (e) {
        console.log('#dropleft-basket span')

    });

    $('#dropleft-basket').click(function (e) {
        e.stopImmediatePropagation();
        console.log('#dropleft-basket')

    });

    $('.hidden-popover-menu').on('click', function (e) {
        console.log('link clicked');

        //$('#keep-open').toggle();
    });
});

