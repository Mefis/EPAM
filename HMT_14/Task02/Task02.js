function findEquation(str) {
    var functions = '+-*/';
    var expression = str.split(' ').join('');
    var expressionResult = ''; 
    var firstValue = '';
    var firstValueComplete = false;
    var secondValue = '';
    var i = 0;
    while (expression[i] != '=') {
        if (functions.indexOf(expression[i]) > -1) {
            firstValue = eval(firstValue);
            firstValue += expression[i];
        }
        else {
            firstValue += expression[i];
        }
        i++;
    }
    firstValue = eval(firstValue);
    return Number((firstValue).toFixed(2));
}