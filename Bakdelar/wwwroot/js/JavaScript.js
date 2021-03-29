$.validator.methods.number = function (value, element) {
    return this.optional(element)/* || /\d{1,3}(,{1}\d{1,2}){0,1}/.test(value)*/;
}
////$.validator.methods.number = function (value, element) {
////    return this.optional(element) || /\d{1,3}(,{1}\d{1,2}){0,1}/.test(value);
////}
//jQuery.validator.addMethod("greaterThanZero", function (value, element, parameter) {
//    console.log("YAY");
//    return this.optional(element) || (parseFloat(value) < parseFloat(parameter));
//}, "* Amount must be greater than zero");
//$('#rea').validate({
//    rules: {
//        amount: { greaterThanZero: true }
//    }
//});
//$("formEdit").validate(function (value) {
//    console.log("YAY");

    //var number1 = parseInt($('.number1').text());
    //var number2 = parseInt($('.number2').text());

    //if (number1 === number2) {
    //    $('.test').css('background-color', 'green');
    //    $('.result').html('It is the same number!');
    //} else {
    //    $('.test').css('background-color', 'red');
    //    $('.result').html('The numbers are different!');
    //}
//})
