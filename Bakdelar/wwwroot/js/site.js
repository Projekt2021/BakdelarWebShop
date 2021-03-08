﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let ShoppingBasketContainer = $('#keep-open');

const HandlerLink = "/shared/AjaxHelper?handler=";


function restore() {
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
            else if (clickTarget == 'javascript:void(1)') {
                e.preventDefault();

                console.log('stopped default!')
                clickTarget = "not void(1) :)"
                console.log(clickTarget);

            }
            else {
                console.log('default executed!')
            }
        });

    });
}




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
                

                console.log('stopped default!')
                clickTarget = "not void(0) :)"
                console.log(clickTarget);
            }
            else if (clickTarget == 'javascript:void(1)')
            {
                e.preventDefault();
                console.log('stopped default!')
                clickTarget = "not void(1) :)"
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
    restore();
});










function updateBasket() {

    let url = HandlerLink+"UpdateBasket";
    var request_method = 'post'; //get form GET/POST method
    var form_data = $('#buyForm').serialize(); //Encode form elements for submission
    console.log(form_data);
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        $('.inner-basket').html(response);
        //console.log(response);
        getItemCount()
        restore();
    });
}










function removeItemDropdown(id) {
    let url = HandlerLink+"RemoveItem";
    event.preventDefault(); //prevent default action
    var request_method = 'post'; //get form GET/POST method
    var form_data = { "RemoveItemID" : id }
    console.log(form_data);
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //

        $('[data-toggle="popover-remove-item-' + id + '"]').popover('hide')
        $('.inner-basket').html(response);
        getItemCount()
        console.log(response);
        restore();
    });
}




function getItemCount() {
    let url = HandlerLink + "ItemCount";
    event.preventDefault(); //prevent default action
    var request_method = 'post'; //get form GET/POST method
    var form_data = {}
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //

        $('#item-count-basket').html(response);
        console.log(response);
        restore();
    });
}