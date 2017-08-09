var page;
var timer;
var timerRunning = false;
var secs = 5;

function setPage(num){
    page = num;
}

function countDown() {
    var status = document.getElementById("status");
    var picture = document.getElementById("picture");
    status.innerHTML = "Next page in " + secs + " seconds."
    picture.innerHTML = '<img src="images/Page0' + page + '.png" style="vertical-align: middle">';
    if (secs < 1) {
        clearTimeout(timer);
        if (page === 4) {
            status.innerHTML = '<a href="Page_1.html">Start again</a>';
            status.innerHTML += ' or ';
            status.innerHTML += '<a href="javascript:window.close();">Close tab</a>';
        }
        else {
            page++;
            window.location.href = 'Page_' + page + '.html';
        }
    }
    secs--;
    timer = setTimeout('countDown()', 1000);
    timerRunning = true;
}

function goForward() {
    if (page === 4) {
        alert("Can't go any further.");
    }
    else {
        page++;
        window.location.href = 'Page_' + page + '.html';
    }
}

function goBack() {
    if (page === 1) {
        alert("Can't go any further.");
    }
    else {
        page--;
        window.location.href = 'Page_' + page + '.html';
    }
}

function playPause() {
    if (timerRunning === false) {
        timer = setTimeout('countDown()', 1000);
        timerRunning === true;
    }
    else {
        clearTimeout(timer);
        timerRunning = false
    }
}