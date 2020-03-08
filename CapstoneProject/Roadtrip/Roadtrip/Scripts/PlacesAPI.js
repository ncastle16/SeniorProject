
﻿function establishments() {
    var name1 = document.getElementById('name');
    var radius1 = document.getElementById('numbers');
    var city1 = document.getElementById('city');
    var state1 = document.getElementById('state');
    var name = name1.value;
    var numbers = radius1.value * 1609;
     if (numbers > 40000) {
         numbers = 39999;
     }
    var city = city1.value;
    var state = state1.value;
    console.log(numbers);
    var source = '/Routes/GetEstablishment?name=' + name + '&numbers=' + numbers + '&city=' + city + '&state=' + state;

    $.ajax({
        type: 'GET',
        datatype: 'json',
        url: source,
        success: test,
        error: errorOnAjax
    });
}

function errorOnAjax(data) {
    console.log('Error on AJAX return');
    console.log(data);
}

function test(data) {

    console.log(data);
    showMap(data);

    $('#establishments').empty();
    for (var i = 0; i < data.total; i++) {
        searchedLocations = data;
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

    for (let i = 0; i < searchedLocations.indexs.length; i++) {
        if (searchedLocations.id[i] == id) {
            selectedLocations.name.push(searchedLocations.name[i]);
            selectedLocations.rating.push(searchedLocations.rating[i]);
            selectedLocations.indexs.push(searchedLocations.indexs[i]);
            selectedLocations.latitude.push(searchedLocations.latitude[i]);
            selectedLocations.longitude.push(searchedLocations.longitude[i]);
            selectedLocations.id.push(searchedLocations.id[i]);
        }
    }
    plotMap();
}







function showName(data) {
    
    $('#addLocation').append(`<li class="list-group-item list-group-item-dark" id="${data.names[0]}"">${data.names[0]}<input id="${data.names[0]}" type="button" value="Delete" onclick="removeElement(this.id)"</li>`);
}

function removeElement(elementId) {
    for (let i = selectedLocations.indexs.length; i >= 0; i--) {
        if (elementId == selectedLocations.name[i]) {
            console.log(selectedLocations.name[i]);


            selectedLocations.name.splice(i, 1);
            selectedLocations.latitude.splice(i, 1); 
            selectedLocations.longitude.splice(i, 1); 

            var element = document.getElementById(elementId);
            element.parentNode.removeChild(element);
            //delete selectedLocations.latitude[i];
            //delete selectedLocations.longitude[i]; 
            plotMap(); 
        }
    }
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

    for (var i = 0; i < data.total; i++) {

        array.push(L.marker([data.latitude[i], data.longitude[i]]).bindPopup(`<b>${data.name[i]}</b></br><input id="${ data.id[i] }" type="button" value="Add" onclick="addName(this.id)">`).addTo(mymap));
    }

    var group = new L.featureGroup(array);
    mymap.fitBounds(group.getBounds());


}

function plotMap(data) {
    document.getElementById('routemap').innerHTML = "<div id='rmap' style='width: 100%; height: 100%;'></div>";
    var mymap = L.map('rmap').setView([selectedLocations.latitude[0], selectedLocations.longitude[0]], 13);


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
    routewps = [];

    for (var i = 0; i < selectedLocations.name.length; i++) {
        array.push(L.marker([selectedLocations.latitude[i], selectedLocations.longitude[i]]).bindPopup("<b>" + selectedLocations.name[i] + "</b>").addTo(mymap));
        routewps.push(L.latLng([selectedLocations.latitude[i], selectedLocations.longitude[i]]));
    }

    var group = new L.featureGroup(array);

    var control = L.Routing.control({
        waypoints: routewps,
        units: 'imperial',
        router: L.Routing.mapbox('pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw')
    }).addTo(mymap);
    control.hide();
    mymap.fitBounds(group.getBounds());

    getDistance([selectedLocations.latitude[0], selectedLocations.longitude[0]], [selectedLocations.latitude[1], selectedLocations.longitude[1]]);
}

function getDistance(wp1, wp2)
{
    var wayPoint1 = L.latLng(wp1[0], wp1[1]);
    var wayPoint2 = L.latLng(wp2[0], wp2[1]);

    rWP1 = new L.Routing.Waypoint;
    rWP1.latLng = wayPoint1;

    rWP2 = new L.Routing.Waypoint;
    rWP2.latLng = wayPoint2;  

    var myRoute = L.Routing.osrmv1();
    myRoute.route([rWP1, rWP2], function (err, routes) {
        distance = routes[0].summary.totalDistance;
        console.log('routing distance: ' + distance);
    });
}
