function maxOfFive(str) {
    var numArr = str.match(/-?\d+/g);
    $("#maxRes").html("Largest: "+Math.max(numArr[0], numArr[1], numArr[2], numArr[3], numArr[4]));
}

function operations(str) {
    var nums = str.match(/-?\d+/g);
    var s = 0;
    var p = 1;
    for (i = 0; i < nums.length; i++) {
        s += Number(nums[i]);
        p *= nums[i];
    }
    $("#sumRes").html("Sum: "+s);
    $("#prodRes").html("Product: "+p);
}

function factorial(num1) {
    var n = num1;
    if (n == 1)
        return 1;
    else
        return n * factorial(n - 1);
}

function palindrome(str) {
    var text = str.replace(/[^0-9A-Za-z]*/g, '').toLowerCase();
    var revStr = text.split('').reverse().join('');
    $('#palinRes').html(revStr == text ? str + " is a palindrome." : str + " is not a palindrome.");
}

function fizzbuzz() {
    var ans = "";
    for (var i = 1; i <= 100; i++) {
        if (i % 15 == 0) {
            ans += "FizzBuzz, ";
        }
        else if (i % 3 == 0) {
            ans += "Fizz, ";
        }
        else if (i % 5 == 0) {
            ans += "Buzz, ";
        }
        else
            ans += i + ", ";
    }
    $('#fizzbuzzResults').html(ans);
}

function perfectNumbers(x) {
    var divArr = new Array();
    for (var i = 1; i <= x / 2; i++) {
        if (x % i == 0) {
            divArr.push(i);
        }
    }
    var sum = 0;
    for (var i = 0; i < divArr.length; i++) {
        sum += divArr[i];
    }
    if (sum == x) {
        return true;
    }
    else
        return false;
}
function perfectNumberRun() {
    var perNums = new Array();
    for (var i = 1; i <= 10000; i++) {
        if (perfectNumbers(i))
            perNums.push(i);
    }
    return perNums.join(', ');
}

function happyNums(n) {
    var m, digit;
    var cycle = [];

    while (n != 1 && cycle[n] !== true) {
        cycle[n] = true;
        m = 0;
        while (n > 0) {
            digit = n % 10;
            m += digit * digit;
            n = (n - digit) / 10;
        }
        n = m;
    }
    return (n == 1);
}

function armstrong() {
    var armArr = new Array();
    var b, c;
    for (var i = 100; i < 1000; i++) {
        var ti = i, c = 0;
        while (ti > 0) {
            b = ti % 10;
            c = c + (b * b * b);
            ti = parseInt(ti / 10);
        }
        if (c == i) {
            armArr.push(i);
        }
    }
    return armArr.join(', ');
}

function longestWord() {
    var f = $("#file")[0];
    var r = new FileReader();
    r.onload = function (e) {
        $('#longestRes').html(findLongestWord(r.result));
    }
    r.readAsText(f.files[0]);
}
function findLongestWord(str) {
    var words = str.match(/\w[a-zA-Z'-]{0,}/gi);
    var longest = words[0];
    for (var x = 1; x < words.length; x++) {
        if (longest.length < words[x].length) {
            longest = words[x];
        }
    }
    return longest;
}

function filterWords() {
    var f = $("#filterFile")[0];
    var r = new FileReader();
    r.onload = function (e) {
        $('#filterRes').html(filterLongWords($('#filterNum').val(), r.result));
    }
    r.readAsText(f.files[0]);
}
function filterLongWords(num, str) {
    var words = str.toLowerCase().match(/\w[a-zA-Z'-]{0,}/gi);
    var arr = [];
    var count = 0;
    for (var n = 0; n < words.length; n++) {
        if (words[n].length > num) {
            count++;
            arr.push(words[n]);
        }
    }
    arr = uniq(arr).sort().join(', ');
    $('#filterDis').html(arr)
    return count + " (non-unique) words longer than " + num;
}

function uniq(arr) {
    var unique = [];
    var hash = {};
    for (var i = 0; i < arr.length; i++) {
        if (!(arr[i] in hash)) {
            hash[arr[i]] = true;
            unique.push(arr[i]);
        }
    }
    return unique;
}

function wordFreq() {
    var f = $("#freqFile")[0];
    var r = new FileReader();
    r.onload = function (e) {
        $("#freqDis").html(wordFreqFinder(r.result));
    }
    r.readAsText(f.files[0]);
}
function wordFreqFinder(str) {
    var words = str.toLowerCase().match(/\w[a-z]{0,}/gi);  
    var hash = {};
    for (var n in words) {            
        if (hash[words[n]] == null) {  
            (hash[words[n]]) = 1      
        } else {
            (hash[words[n]])++         
        }
    };
    var wordsArranged = [];
    for (var n in hash) {              
        wordsArranged.push([n, hash[n]]);
    };
    wordsArranged.sort(function(a, b) { 
        return b[1] - a[1]
    });
    var result = "";
    for (var n in wordsArranged) {
        result = result.replace(",",":") + '\n' + wordsArranged[n];
    };
    return result;
}

function findWord(search) {
    var f = $("#findFile")[0];
    var r = new FileReader();
    r.onload = function (e) {
        $("#findRes").html(searchWord(search, r.result));
    }
    r.readAsText(f.files[0]);
}
function searchWord(search, str) {
    var words = str.toLowerCase().match(/\w[a-z-]{0,}/gi);
    var searchS = search.toString().toLowerCase();
    var count = 0;
    for (var n = 0; n < words.length; n++) {
        if (searchS == words[n])
            count++;
    }
    return search + " appears " + count + " times."
}