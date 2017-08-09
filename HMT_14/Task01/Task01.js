function findUniqueCharacters(str) {
    var uniqueList = str.split(' ');
    var uniqueListResult = '';
    var repeatingValues = '';
    var uniqueWord = '';
    var word = '';
    for (var i = 0; i < uniqueList.length; i++) {
        word = uniqueList[i];
        for (var j = 0; j < word.length; j++) {
            if (word.lastIndexOf(word[j]) != word.indexOf(word[j])) {
                repeatingValues += word[j];
            }
        }
    }
    for (var i = 0; i < uniqueList.length; i++) {
        word = uniqueList[i];
        for (var j = 0; j < word.length; j++) {
            if (!(repeatingValues.indexOf(word[j]) > -1)) {
                uniqueWord += word[j];
            }
        }
        uniqueListResult += ' ' + uniqueWord;
        uniqueWord = '';
    }
    return uniqueListResult;
}