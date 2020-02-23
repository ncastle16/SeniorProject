$(document).ready(function () {

    $.ajax({
        type: 'GET',
        datatype: 'json',
        url: '/Routes/GetEstablishment',
        success: test,
        error: errorOnAjax
    });
});

function errorOnAjax(data) {
    console.log('Error on AJAX return');
    console.log(data);
}

function test(data) {

    showMap(data);

    console.log('yay');
    console.log(data);
    for (var i = 0; i < 20; i++) {
        $('#establishments').append(`
<div style="display:table; width:100%">
        <div style="display: table-row">
            <div style="width: 400px; display: table-cell; background-color:antiquewhite; border:1px solid black">
                <ul>${data.name[i]}</ul> 
                <input id="${data.id[i]}" type="button" value="Get Details" onclick="details(this.id)">
                <input id="${data.id[i]}" type="button" value="Add Location" onclick="addName(this.id)">

            </div>
        </div>
</div>
`);
 }
}

function details(id) {
    var source = '/Routes/GetDetails?id=' + id;

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showDetails,
        error: errorOnAjax
    });
}

function details(id) {
    var source = '/Routes/GetDetails?id=' + id;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showDetails,
        error: errorOnAjax
    });
}
function showDetails(data) {
    console.log(data);
    $('#details').empty();
    $('#details').append(`<div style="margin-top:50px;margin-bottom:50px;"><img src="${data.image[0]}" style="width:200px;height:150px;"><br> <b>${data.names[0]}</b><br>This business has a rating of ${data.ratings[0]}<br> Located at: ${data.addresss[0]}  ${data.citys[0]}, ${data.states[0]} ${data.zipcodes[0]}<br>The phone number for this business is: ${data.phones[0]}</div>`);
}



function addName(id) {

    var source = '/Routes/GetDetails?id=' + id;
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showName,
        error: errorOnAjax
    });
}

function showName(data) {
    console.log(data);
    $('#addLocation').append(`<div style="margin-top:10px;margin-bottom:10px;"><b>${data.names[0]}</b><br></div>`);
}

    




function showMap(data) {
    document.getElementById('searchmap').innerHTML = "<div id='smap' style='width: 100%; height: 100%;'></div>";
    var mymap = L.map('smap').setView([data.latitude[0], data.longitude[0]], 13);


    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
        maxZoom: 18,
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
            '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
            'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1
    }).addTo(mymap);

    var array = [];

    for (var i = 0; i < 20; i++) {
        array.push(L.marker([data.latitude[i], data.longitude[i]]).bindPopup("<b>" + data.name[i] + "</b>").addTo(mymap));
    }

    var group = new L.featureGroup(array);
    mymap.fitBounds(group.getBounds());


}