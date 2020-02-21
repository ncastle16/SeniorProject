$(document).ready(function () {
    var mymap = L.map('searchmap').setView([44, -122], 13);
    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
        maxZoom: 18,
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
            '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
            'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1
    }).addTo(mymap);
});

function getInfo() {
    var myInfo = document.getElementById('name').value;
    var city = document.getElementById('city').value;
    var state = document.getElementById('state').value; 
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Routes/DisplayInfo",
        data: { 'myInfo': myInfo, 'city': city, 'state': state },
        success: showMap,
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
function showMap(data) {

    var mymap = L.map('searchmap').setView([data.Lat, data.Lon], 13);


    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
        maxZoom: 18,
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
            '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
            'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1
    }).addTo(mymap);

    var marker = L.marker([data.Lat, data.Lon]).addTo(mymap);


        }