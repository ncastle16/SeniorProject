function toggle(e) {
    var x = document.getElementById(e);
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}
function toggleOn(e) {
    var x = document.getElementById(e);
    x.style.display = "block";
}
$(document).ready(function () {
    toggleOff("saveButton");
    toggleOff("alertboard");
});


function toggleOff(e) {
    var x = document.getElementById(e);
    x.style.display = "none";
}

function saveRoute() {
    if (confirm("Are you sure you want to save this route?")) {
        var savedList = new Array();
        
        for (var i = 0; i < selectedLocations.name.length; i++) {
            savedList.push({
                Name: selectedLocations.name[i],
                Latitude: selectedLocations.latitude[i],
                Longitude: selectedLocations.longitude[i],
                Id: selectedLocations.id[i]
            });
        }
        console.log(savedList);

        document.getElementById('alertboard').innerHTML = "<div id='panelinner'>SAVING...</div>";
        toggleOff("panel");
        toggleOn("alertboard");
        $.ajax({
            type: "POST",
            url: "/SavedRoutes/SaveRoute",
            data: JSON.stringify(savedList),
            success: function (response) {
                console.log("Data saved successfully");
                alert("Route saved successfully!");
                document.getElementById('alertboard').innerHTML = "";
                document.getElementById('alertboard').innerHTML = "<div id='panelinner'>SAVED!</div>";
                toggleOn("panel");
                toggleOff("alertboard");
                //if (response.result == 'Redirect')
                //  window.location = response.url;
            },
            error: errorOnAjax,
            dataType: "json",
            contentType: 'application/json',
            traditional: true
        });
    }
}

function establishments() {
    var name1 = document.getElementById('name');
    var radius1 = document.getElementById('numbers');
    var city1 = document.getElementById('city');
    var state1 = document.getElementById('state');
    var name = name1.value;
    var numbers = radius1.value * 1609;
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
    $('#details').append(`<div style="margin-top:50px;margin-bottom:50px;"><img src="${data.image[0]}" style="width:200px;height:150px;"><br> <b>${data.names[0]}</b><br>This business has a rating of ${data.ratings[0]}<br> Located at: ${data.addresss[0]}  ${data.citys[0]}, ${data.states[0]} ${data.zipcodes[0]}<br>The phone number for this business is: ${data.phones[0]}<input id="${data.id}" type="button" value="MoreDetails" onclick="moreDetails(this.id)"></div>`);
}

function moreDetails(id) {
    var source = '/Routes/GetMoreDetails?id=' + id;

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showMoreDetails,
        error: errorOnAjax
    });
}

function showMoreDetails(data) {
    console.log(data);
    for (var i = 0; i < 3; i++) {
        
        $('#details').append(`<div style="margin-top:50px;margin-bottom:50px;"><img src="${data.image[i]}" style="width:200px;height:150px;"><br><b>${data.name[i]}</b><br>${data.text[i]}<br><b>This user has a rating of</b> ${data.rating[i]}<br></div>`);
    }
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

    if (selectedLocations.name.length > 1)
        toggleOn("saveButton");


    plotMap();
}







function showName(data) {
    
    $('#addLocation').append(`<li class="ui-state-default" id="${data.names[0]}""><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>${data.names[0]}<input id="${data.names[0]}" type="button" value="Delete" onclick="removeElement(this.id)"</li>`);
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
            if (selectedLocations.name.length < 2)
                toggleOff("saveButton");

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

function getDistance(rwp1, rwp2) {
    var source = 'https://router.project-osrm.org/route/v1/driving/' + rwp1[1] + ',' + rwp1[0] + ';' + rwp2[1] + ',' + rwp2[0] + '?overview=false';

    return JSON.parse($.ajax({
        type: 'GET',
        dataType: 'application/json',
        url: source,
        async: false
    }).responseText).routes[0].distance;
}


function newA(arr, x) {
    arr = new Array();
    for (var i = 0; i < x; i++) {
        arr[i] = new Array();
    }
    return arr;
}

function getType(o) {
    var _t;
    return ((_t = typeof(o)) == "object" ? o==null && "null" || Object.prototype.toString.call(o).slice(8,-1):_t).toLowerCase();
}
function extend(destination, source) {
    for (var p in source) {
        if (getType(source[p]) == "array" || getType(source[p]) == "object") {
            destination[p] = getType(source[p]) == "array" ? [] : {};
            arguments.callee(destination[p], source[p]);
        }
        else {
            destination[p] = source[p];
        }
    }
}

function totalR(r) {
    var s = 0; 
    var l = r.length;
    for (var i = 0; i < l; i++) {
        s += r[i];
    }
    return s;
}

function ACS() {
    var cn = selectedLocations.name.length;
    var an = 10;
    var p = 0.7;
    var iter = 1;
    var itermax = 10;
    var dis;
    dis = newA(dis, cn);
    var Ax;
    Ax = newA(Ax, cn);
    var Rx;
    Rx = newA(Rx, cn);
    var shortdis = 999999999;
    var shortestR = new Array();
    shortestR[0] = new Array();
    
    for (var i = 0; i < cn; i++) {
        for (var j = i + 1; j < cn; j++) {
            var distance = getDistance([selectedLocations.latitude[i], selectedLocations.longitude[i]], [selectedLocations.latitude[j], selectedLocations.longitude[j]]);
            dis[i][j] = distance;
            dis[j][i] = dis[i][j];
        }
        dis[i][i] = 0;
    }
    
    for (var i = 0; i < cn; i++) {
        for (var j = i + 1; j < cn; j++) {
            Ax[i][j] = 1 / (cn * (cn - 1));
            Ax[j][i] = Ax[i][j];
        }
        Ax[i][i] = 0;
    }

    extend(Rx, Ax);

    while (iter <= itermax) {
        
        var road = new Array();

        road[0] = new Array();

        road[0][0] = new Array();

        for (var k = 0; k < an; k++) {
            road[k] = new Array();
            for (var k5 = 0; k5 < cn; k5++) {
                road[k][k5] = new Array();
                for (var k3 = 0; k3 < 1; k3++) {
                    road[k][k5][k3] = 0;
                    road[k][k5][k3 + 1] = 1;
                }

            }
        }
        for (var h = 0; h < an; h++) {
            road[h].shift();
            road[h].shift();
            road[h].shift();
        }
        

        for (var ai = 0; ai < an; ai++) {
            extend(Ax, Rx);
            var route = new Array();
            for (var j = 0; j < cn; j++) {
                route[j] = j;
            }
            var pre = 0;
            var next = 0;

            
            

            while (totalR(route) != 0) {

                var temp = Ax[pre];
                var result = new Array();
                var s = 0;
                for (var i = 1; i < cn; i++) {
                    s += temp[i];
                }
                for (var j = 1; j < cn; j++) {
                    if (j == 1) {
                        result[j] = temp[j] / s;

                    } else {
                        result[j] = result[j - 1] + temp[j] / s;
                    }
                }



                var k = Math.random();

                var ri = 0;
                for (ri = 0; ri < cn; ri++) {
                    if (result[ri] > k) {
                        next = ri;
                        break;
                    }
                }


                

                road[ai].push([pre, next]);
                
                route[next] = 0;

                for (var i = 0; i < cn; i++) {
                    Ax[i][next] = 0;
                }
                pre = next;
            }

            next = 0;
            
            road[ai].shift();
            

            
        }

        var rd = new Array();
        for (var i = 0; i < an; i++) {
            rd[i] = 0;
        }
       
        for (var i = 0; i < an; i++) {
            for (var j = 0; j < cn-1; j++) {
                
               rd[i] += dis[road[i][j][0]][road[i][j][1]];
                
            }
        }


        
        var min = rd[0];
        var temp1 = 0;
        var shorter = min;
        for (var i = 0; i < an; i++) {
            if (min > rd[i]) {
                shorter = rd[i];
                temp1 = i;
            }
        }
       
       
        if (shortdis > shorter) {
            shortdis = shorter;
            shortestR = road[temp1];
        }

        for (var i = 0; i < cn-1; i++) {
            Rx[road[temp1][i][0]][road[temp1][i][1]] = Rx[road[temp1][i][0]][road[temp1][i][1]] * (1 - p) + p * (1 / shorter);
        }
        for (var i = 0; i < cn-1; i++) {
            for (var j = 0; j < cn-1; j++) {
                if (j != road[temp1][i][1]) {
                    Rx[i][j] = Rx[i][j] * (1 - p);
                }
            }
        }
        iter++;
    }
    var text = "";
    for (var i = 0; i < cn - 1; i++) {
        text += "#distance:" + dis[shortestR[i][0]][shortestR[i][1]] +"<br>" + "("+(i+2)+")" + selectedLocations.name[shortestR[i][1]] + "<br>"
    }
    document.getElementById("short").innerHTML = "(1)" + selectedLocations.name[shortestR[0][0]] + "<br>" + text;


    document.getElementById('routemap').innerHTML = "<div id='rmap' style='width: 100%; height: 100%;'></div>";
    var mymap = L.map('rmap').setView([selectedLocations.latitude[shortestR[0][0]], selectedLocations.longitude[shortestR[0][0]]], 13);

    
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

    array.push(L.marker([selectedLocations.latitude[shortestR[0][0]], selectedLocations.longitude[shortestR[0][0]]]).bindPopup("<b>" + selectedLocations.name[shortestR[0][0]] + "</b>").addTo(mymap));
    routewps.push(L.latLng([selectedLocations.latitude[shortestR[0][0]], selectedLocations.longitude[shortestR[0][0]]]));

    for (var i = 0; i < cn-1; i++) {
        array.push(L.marker([selectedLocations.latitude[shortestR[i][1]], selectedLocations.longitude[shortestR[i][1]]]).bindPopup("<b>" + selectedLocations.name[shortestR[i][1]] + "</b>").addTo(mymap));
        routewps.push(L.latLng([selectedLocations.latitude[shortestR[i][1]], selectedLocations.longitude[shortestR[i][1]]]));
    }
    

    var group = new L.featureGroup(array);

    var control = L.Routing.control({
        waypoints: routewps,
        units: 'imperial',
        router: L.Routing.mapbox('pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw')
    }).addTo(mymap);
    control.hide();
    mymap.fitBounds(group.getBounds());
}