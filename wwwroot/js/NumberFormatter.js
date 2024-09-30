var NumberFormatter = (function () {
    var ThousandSeparator = function (x) {
        if (x != null) {
            var xStr = (x).toFixed(2);
            return xStr.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }       
    }
    var ThousandSeparator4Digit = function (x) {
        if (x != null) {
            var xStr = (x).toFixed(4);
            return xStr.replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ",");
        }
    }

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            i.replace(/[\$,]/g, '') * 1 :
            typeof i === 'number' ?
                i : 0;
    };

    return {
        ThousandSeparator: ThousandSeparator,
        ThousandSeparator4Digit: ThousandSeparator4Digit,
        IntVal: intVal
    }
})();