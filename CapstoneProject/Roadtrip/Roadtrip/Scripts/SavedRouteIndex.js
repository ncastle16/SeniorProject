$(document).ready(function () {
    if (RouteList.length > 0) {
        $('#indexList').empty();
        addToIndexList();
    }
});

function addToIndexList() {

    if (RouteList.length < loadItems)
        loadItems = RouteList.length;

    for (var i = paginationPosition; i < paginationPosition + loadItems; i++) {
        $('#indexList').append(`
            <div class= "row" style="border-style: solid; border-color: darkslategrey;"> <div style='width: 5%; height: 30vh;'>${i + 1}.</div>
                <div id='rmap${i}' style='width: 60%; height:30vh; border-style: solid; border-color: darkseagreen;'></div>
                <div style='width: 35%; height:30vh; text-align: center; padding-top: 13vh;'><i>By ${RouteList[i].Username}</i></div>
            </div>
            `);
        showRoute(i, 'rmap' + i);
    }
    paginationPosition += loadItems;

    if (paginationPosition + loadItems > RouteList.length)
        loadItems = RouteList.length - paginationPosition;

    document.querySelector('#loadMore').value = "Load next " + loadItems + " routes [" + (RouteList.length - paginationPosition) + " left]";

    if (loadItems == 0)
        toggleOff("loadMore");
}

function toggleOn(e) {
    var x = document.getElementById(e);
    x.style.display = "block";
}
function toggleOff(e) {
    var x = document.getElementById(e);
    x.style.display = "none";
}

function showRoute(id, divid) {

var mymap = L.map(divid).setView([45, -123], 13);

    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
        maxZoom: 18,
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
            '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
            'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1
    }).addTo(mymap);

    var route = [];
    routewps = [];

    for (var i = 0; i < RouteList[id].Locations.length; i++) {
        routewps.push(L.latLng([RouteList[id].Locations[i].Latitude, RouteList[id].Locations[i].Longitude]));
    }

    var control = L.Routing.control({
        waypoints: routewps,
        units: 'imperial',
        router: L.Routing.mapbox('pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw')
    }).addTo(mymap);
    control.hide();

    for (var i = 0; i < RouteList[id].Locations.length; i++) {
        route.push(L.marker([RouteList[id].Locations[i].Latitude, RouteList[id].Locations[i].Longitude]).bindPopup("<b>" + RouteList[id].Locations[i].Name + "</b>").addTo(mymap));
    }

    var group = new L.featureGroup(route);

    mymap.fitBounds(group.getBounds());
}