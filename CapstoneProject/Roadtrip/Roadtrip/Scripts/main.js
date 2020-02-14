

function getInfo() {
    var myInfo = document.getElementById('name').value;
    var city = document.getElementById('city').value;
    var state = document.getElementById('state').value; 
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Routes/DisplayInfo",
        data: { 'myInfo': myInfo, 'city': city, 'state': state },
        success: showInfo,
        error: errorOnAjax
    });
}
function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function showInfo(data) {
   
    $('#outputTable').append($('<div id=\"displayMap\">'));
    $('#displayMap').append($('<h3> MAP </h3>'));
    $('#displayMap').append($('<iframe id=\"mapLoc\">'));
    $('#displayMap').append($('</iframe>'));
    $('#displayMap').append($('</div>'));
    var first = document.getElementById("mapLoc");

    first.src = data.URL;
    first.width = "600"; 
    first.height = "450"; 
}