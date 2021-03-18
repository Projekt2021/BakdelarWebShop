﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let ShoppingBasketContainer = $('#keep-open');
let currentPage = window.location.pathname;
const HandlerLink = "/shared/AjaxHelper?handler=";


//function updatePagePostAjax(id, handler, data, htmlElement, popoverhide = false) {
//    let url = HandlerLink + handler;
//    event.preventDefault(); //prevent default action
//    var request_method = 'post'; //get form GET/POST method
//    var form_data = data;





//    console.log(form_data);
//    $.ajax({
//        url: url,
//        type: request_method,
//        data: form_data,
//        beforeSend: function (xhr) {
//            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
//        },
//    }).done(function (response) { //
//        if (popoverhide == true) {
//            $('[data-toggle="popover-remove-item-' + id + '"]').popover('hide')
//        }
//        htmlElement.html(response);
//        getItemCount();
//        if (onProductPage(id) == true) {
//            getNumberInStock(id);
//        }
//        console.log(response);
//        restore();
//    });
//}



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
                e.stopPropagation();

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

    $(".form-inline .form-control").change(function () {
        let itemID = $(this).attr("id").split("-")[2];
        let newAmount = $(this).val();
        if (newAmount == 0 || "") {
            removeItemDropdown(itemID);
        }
        else {
        console.log(itemID);
            changeItemCount(itemID, newAmount)
        }
    });
}


//function changeItemCount(itemID1, amount) {

//    let url = HandlerLink + "UpdateItemCountShoppingBasket";
//    var request_method = 'post'; //get form GET/POST method
//    let item = Number(itemID1);
//    var form_data =
//    {
//        itemID: itemID1,
//        newAmount: amount
//    }; //Encode form elements for submission
//    console.log(form_data)
//    console.log(form_data);
//    $.ajax({
//        url: url,
//        type: request_method,
//        data: form_data,
//        beforeSend: function (xhr) {
//            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
//        },
//    }).done(function (response) { //
//        if (response.length > 2) {

//            $('.shopping-basket-table').html(response);
//            getItemCount();
//            getTotalCost();
//            restore();
//        }
//        else {
//            updateBasketDropdown(id);
//        }
//    });
//}




function changeItemCount(itemID1, amount) {

    let url = HandlerLink + "ChangeCountItem";
    var request_method = 'post'; //get form GET/POST method
    var form_data =
    {
        id: itemID1,
        newAmount: amount
    }; //Encode form elements for submission
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        dataType: 'json',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        let itemCost = response.costItems;
        $('#total-price-' + itemID1).html(itemCost);
        getItemCount();
            getTotalCost();
            getShippingCostBanner();
        updateBasketDropdown(itemID1);
        getShippingCostRow();
    });
}



function getShippingCostRow() {

    let url = HandlerLink + "GetShippingCostRow";
    var request_method = 'post'; //get form GET/POST method
    var form_data = {}//Encode form elements for submission
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        $('#shipping-cost-row').html(response);
    });
}


function getShippingCostBanner() {

    let url = HandlerLink + "GetShippingCostBanner";
    var request_method = 'post'; //get form GET/POST method
    var form_data = {}; //Encode form elements for submission
    console.log(form_data);
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        $('#shipping-cost-banner').replaceWith(response);
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
            trigger: 'focus',
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
                e.stopPropagation();

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







function closePopover_Dropdown(id) {
    let thisElement = $('[data-toggle="popover-remove-item-dropdown-' + id + '"]');
    thisElement.popover('hide');


}





$(document).ready(function () {
    restore();
});





function getTotalCost() {

    let url = HandlerLink + "GetTotalCost";
    var request_method = 'post'; //get form GET/POST method
    var form_data = {}; //Encode form elements for submission
    console.log(form_data);
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        $('.shopping-basket-total').html(response);
        if (currentPage == "/ShoppingBasket") {
            $("#total-cost-below").html(response);
        }
        //restore();
    });
}









function reduceByOne(id) {

    let url = HandlerLink + "DecreaseByOne";
    var request_method = 'post'; //get form GET/POST method
    var form_data = { UpdateCountID: id }; //Encode form elements for submission
    console.log(form_data);
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        if (response.length > 2) {

            $('.item-dropdown-' + id).html(response);
            getItemCount();
            if (onProductPage(id) == true) {
                getNumberInStock(id);
            }
            getTotalCost();
            //restore();
        }
        else {
            updateBasketDropdown(id);
        }
        if (currentPage == "/ShoppingBasket") {
            console.log("current page is shopping basket")
            updateShoppingBasketPage();
        }
    });
}












function increaseByOne(id) {
    let url = HandlerLink + "IncreaseByOne";
    var request_method = 'post'; //get form GET/POST method
    var form_data = { UpdateCountID: id }; //Encode form elements for submission
    console.log(form_data);
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //

        if (response.length > 2) {
            $('.item-dropdown-' + id).html(response);
            getItemCount();
            if (onProductPage(id) == true) {
                getNumberInStock(id);
            }
            getTotalCost();
            //restore();
        }

        else {
            updateBasketDropdown(id);
        }
        if (currentPage == "/ShoppingBasket") {
            console.log("current page is shopping basket")
            updateShoppingBasketPage();
        }
    });
}

function getNumberInStock(id) {

    let url = HandlerLink + "GetNumberInStock";
    var request_method = 'post'; //get form GET/POST method
    var form_data = { UpdateStockProductID: id }; //Encode form elements for submission
    console.log(form_data);
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        $('#product-buy-options').html(response);
        //restore();
    });
}


function updateBasketDropdown(id) {

    let url = HandlerLink + "UpdateBasket";
    var request_method = 'post'; //get form GET/POST method
    var form_data = {} //Encode form elements for submission
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        $('.inner-basket').html(response);
        console.log(response);

        if (onProductPage(id) == true) {
            getNumberInStock(id);
        }
        getItemCount();
        //restore();
        getTotalCost();
    });
}

function updateBasket(id) {

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
        getItemCount();
        getNumberInStock(id);
        //restore();
        getTotalCost();
    });
}










function removeItemDropdown(id) {
    let idnr = id;
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
        getItemCount();
        if (onProductPage(id) == true) {
            getNumberInStock(id);
        }
        //restore();
        if (currentPage == "/ShoppingBasket") {
            updateShoppingBasketPage()

        }
    });
}

function onProductPage(id) {
    let path = window.location.pathname;
    let pathSplit = path.split('/');
    if (pathSplit.length == 3) {
        if (pathSplit[1] == 'SingleProductView') {
            let idCompare = pathSplit.slice(-1)[0];
            if (idCompare == id) {
                return true;
            }
        }
    }
    return false;
}



function getItemCount() {
    let url = HandlerLink + "ItemCount";
    //event.preventDefault(); //prevent default action
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
        if (response.length == 0) {
            getItemCount();
        }
        else {
        $('#item-count-basket').html(response);
            //restore();
        }
    });
}



function updateShoppingBasketPage() {

    let url = HandlerLink + "UpdateShoppingBasketPage";
    var request_method = 'post'; //get form GET/POST method
    var form_data = {}; //Encode form elements for submission
    $.ajax({
        url: url,
        type: request_method,
        data: form_data,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        },
    }).done(function (response) { //
        $('.shopping-basket-table').html(response);
        //console.log(response);
        restore();
    });
}
