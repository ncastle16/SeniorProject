$(document).ready(function () {
    if (RouteList.length > 0) {
        $('#indexList').empty();
        addToIndexList();
    }
});

function addToIndexList() {

    if (RouteList.length < loadItems)
        loadItems = RouteList.length

    for (var i = paginationPosition; i < paginationPosition + loadItems; i++) {
        $('#indexList').append(`
            <div id="${RouteList[i].SRID}" class= "row" style="border-style: solid; border-color: darkslategrey;"> <div style='width: 5%; height: 30vh;'>${i + 1}.</div>
                <div id='rmap${i}' style='width: 60%; height:30vh; border-style: solid; border-color: darkseagreen;'></div>
                <div style='width: 35%; height:30vh; text-align: center; padding-top: 13vh;'><i>By ${RouteList[i].Username} <br> 
                Route Name: ${RouteList[i].routeName}</i>
                <br> 
                Tags: ${RouteList[i].Tag1}, ${RouteList[i].Tag2}</div>
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


    /*var iy = tryingWork();
    if (iy != null) {
        //console.log(iy);
        var elem = document.getElementById(iy);
        console.log(elem);
        var topPos = elem.offsetTop;


        scrollTo(document.getElementById('indexList'), topPos - 5, 500);

        highlight(iy, "#ffff00");
        setTimeout(function () { highlight(iy, "#FEFD1B"); }, 500);
        setTimeout(function () { highlight(iy, "#FEFA36"); }, 600);
        setTimeout(function () { highlight(iy, "#FDF851"); }, 700);
        setTimeout(function () { highlight(iy, "#FDF56C"); }, 800);
        setTimeout(function () { highlight(iy, "#FCF386"); }, 900);
        setTimeout(function () { highlight(iy, "#FBF0A1"); }, 1000);
        setTimeout(function () { highlight(iy, "#FBEEBC"); }, 1100);
        setTimeout(function () { highlight(iy, "#FAEBD7"); }, 1200);



    }*/
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

function tryingWork() {
    let params = new URLSearchParams(location.search);
    var my = params.get("ID");

    return my;
}
function scrollTo(element, to, duration) {
    console.log(element);
    var start = element.scrollTop,
        change = to - start,
        currentTime = 0,
        increment = 20;

    var animateScroll = function () {
        currentTime += increment;
        var val = Math.easeInOutQuad(currentTime, start, change, duration);
        element.scrollTop = val;
        if (currentTime < duration) {
            setTimeout(animateScroll, increment);
        }
    };
    animateScroll();

}

Math.easeInOutQuad = function (t, b, c, d) {
    t /= d / 2;
    if (t < 1) return c / 2 * t * t + b;
    t--;
    return -c / 2 * (t * (t - 2) - 1) + b;
};

function highlight(name, color) {
    var a = document.getElementById(name);
    console.log(a);
    a.style.backgroundColor = color;
}