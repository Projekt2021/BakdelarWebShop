$.validator.methods.number = function (value, element) {
    return this.optional(element) || /\d{1,3}(,{1}\d{1,2}){0,1}/.test(value);
}

       
