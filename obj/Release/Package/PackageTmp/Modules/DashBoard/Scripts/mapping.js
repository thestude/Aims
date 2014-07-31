var map;
MarkerClusterer.prototype.MARKER_CLUSTER_IMAGE_PATH_ =
'Content/images/m';
$(document).ready(function () {
    map = new GMaps({
        div: '#map',
        lat: 32.3181,
        lng: -86.9022,
        zoom: 8,
        mapTypeControlOptions: {
            position: google.maps.ControlPosition.BOTTOM
        },
        markerClusterer: function (map) {
            return new MarkerClusterer(map);
        }
    });
    var lat_span = 31.247876 - 34.247876;
    var lng_span = -85.165771 - -87.165771;

    for (var i = 0; i < 50; i++) {
        var latitude = Math.random() * (lat_span) + 34.247876;
        var longitude = Math.random() * (lng_span) + -87.165771;

        map.addMarker({
            lat: latitude,
            lng: longitude,
            icon: 'Content/images/hospital-pointer-green.png',
            title: 'Marker #' + i,
            infoWindow: {
                content: '<h3><span class="pull-right label label-success">Open</span>COMMUNITY HOSPITAL</h3> <ul class="nav nav-tabs"> <li class="active"><a href="#status" data-toggle="tab">Status</a></li> <li><a href="#contact" data-toggle="tab">Contact</a></li> </ul> <div class="tab-content"> <div class="tab-pane active" id="status"> <table class="table table-striped"> <tr> <th>EMERGENCY DEPARTMENT BEDS:</th> <td>Available: 4</td> <td>Capacity: 5</td> </tr> <tr> <th>ADULT ICU:</th> <td>Available: 0</td> <td>Capacity: 5</td> </tr> <tr> <th>PEDIATRIC MED/SURG:</th> <td>Available: 3</td> <td>Capacity: 3</td> </tr> <tr> <th>STAFFED BEDS:</th> <td>Available: 33</td> <td>Capacity: 47</td> </tr> <tr> <th>NEGATIVE PRESSURE/ISOLATION:</th> <td>Available: 3</td> <td>Capacity: 3</td> </tr> <tr> <th>PSYCHIATRIC BEDS:</th> <td>Available: 0</td> <td>Capacity: 10</td> </tr> </table> </div> <div class="tab-pane" id="contact"> <table class="table table-striped"> <tr> <th>Contact Person:</th> <td>615-555-5151</td> </tr> <tr> <th>Contact Person Two:</th> <td>615-555-5151</td> </tr> <tr> <th>Manager:</th> <td>615-555-5151</td> </tr> </table> </div> </div> '
            }
        });
    }
    for (var i = 0; i < 50; i++) {
        var latitude2 = Math.random() * (lat_span) + 34.247876;
        var longitude2 = Math.random() * (lng_span) + -87.165771;

        map.addMarker({
            lat: latitude2,
            lng: longitude2,
            icon: 'Content/images/hospital-pointer-red.png',
            title: 'Marker #' + i,
            infoWindow: {
                content: '<h3><span class="pull-right label label-important">Closed</span>COMMUNITY HOSPITAL</h3> <ul class="nav nav-tabs"> <li class="active"><a href="#status" data-toggle="tab">Status</a></li> <li><a href="#contact" data-toggle="tab">Contact</a></li> </ul> <div class="tab-content"> <div class="tab-pane active" id="status"> <table class="table table-striped"> <tr> <th>EMERGENCY DEPARTMENT BEDS:</th> <td>Available: 4</td> <td>Capacity: 5</td> </tr> <tr> <th>ADULT ICU:</th> <td>Available: 0</td> <td>Capacity: 5</td> </tr> <tr> <th>PEDIATRIC MED/SURG:</th> <td>Available: 3</td> <td>Capacity: 3</td> </tr> <tr> <th>STAFFED BEDS:</th> <td>Available: 33</td> <td>Capacity: 47</td> </tr> <tr> <th>NEGATIVE PRESSURE/ISOLATION:</th> <td>Available: 3</td> <td>Capacity: 3</td> </tr> <tr> <th>PSYCHIATRIC BEDS:</th> <td>Available: 0</td> <td>Capacity: 10</td> </tr> </table> </div> <div class="tab-pane" id="contact"> <table class="table table-striped"> <tr> <th>Contact Person:</th> <td>615-555-5151</td> </tr> <tr> <th>Contact Person Two:</th> <td>615-555-5151</td> </tr> <tr> <th>Manager:</th> <td>615-555-5151</td> </tr> </table> </div> </div> '
            }
        });
    }
    for (var i = 0; i < 50; i++) {
        var latitude3 = Math.random() * (lat_span) + 34.247876;
        var longitude3 = Math.random() * (lng_span) + -87.165771;

        map.addMarker({
            lat: latitude3,
            lng: longitude3,
            icon: 'Content/images/hospital-pointer-yellow.png',
            title: 'Marker #' + i,
            infoWindow: {
                content: '<h3><span class="pull-right label label-warning">On Generator</span>COMMUNITY HOSPITAL</h3> <ul class="nav nav-tabs"> <li class="active"><a href="#status" data-toggle="tab">Status</a></li> <li><a href="#contact" data-toggle="tab">Contact</a></li> </ul> <div class="tab-content"> <div class="tab-pane active" id="status"> <table class="table table-striped"> <tr> <th>EMERGENCY DEPARTMENT BEDS:</th> <td>Available: 4</td> <td>Capacity: 5</td> </tr> <tr> <th>ADULT ICU:</th> <td>Available: 0</td> <td>Capacity: 5</td> </tr> <tr> <th>PEDIATRIC MED/SURG:</th> <td>Available: 3</td> <td>Capacity: 3</td> </tr> <tr> <th>STAFFED BEDS:</th> <td>Available: 33</td> <td>Capacity: 47</td> </tr> <tr> <th>NEGATIVE PRESSURE/ISOLATION:</th> <td>Available: 3</td> <td>Capacity: 3</td> </tr> <tr> <th>PSYCHIATRIC BEDS:</th> <td>Available: 0</td> <td>Capacity: 10</td> </tr> </table> </div> <div class="tab-pane" id="contact"> <table class="table table-striped"> <tr> <th>Contact Person:</th> <td>615-555-5151</td> </tr> <tr> <th>Contact Person Two:</th> <td>615-555-5151</td> </tr> <tr> <th>Manager:</th> <td>615-555-5151</td> </tr> </table> </div> </div> '
            }
        });
    }

    var styles = [
    {
        stylers: [
        { hue: "#208595" },
        { saturation: -10 }
        ]
    }, {
        featureType: "road",
        elementType: "geometry",
        stylers: [
        { lightness: 50 },
        { visibility: "simplified" }
        ]
    },
    ];
    map.addStyle({
        styledMapName: "Styled Map",
        styles: styles,
        mapTypeId: "map_style"
    });
    map.setStyle("map_style");
    //LEGEND //
    /* commenting out for now so we can move the controls up top
    map.addControl({
    position: 'right_bottom',
    content: '<form><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox" checked><img src="/images/hospital-pointer.png" /> Hospital</label></div><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox"><img src="/images/chc-pointer.png" /> Community Health Center</label></div><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox"><img src="/images/nursing-pointer.png" /> Nursing Home</label></div><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox"><img src="/images/ems-pointer.png" /> EMS</label></div><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox" checked><span class="label label-large label-success">Open</span></label></div><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox" checked><span class="label label-large label-important">Closed</span></label></div><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox" checked><span class="label label-large label-warning">On Generator</span></label></div><div style="border-bottom:1px solid #ddd;padding:2px"><label class="checkbox"><input type="checkbox" checked><span class="label label-large label-blue">On Generator</span></label></div></form>',
    style: {
    margin: '5px',
    padding: '1px 6px',
    border: 'solid 1px #717B87',
    background: '#fff'
    },
    events: {
    click: function(){
    console.log(this);
    }
    }
    });
    */
    $('#map-tools').on('show.bs.collapse hide.bs.collapse', function (e) {
        if ($(e.target).attr("id") != "map-tools") return;
        $('#collapse-icon').toggleClass('fa-chevron-down fa-chevron-up', 200);
    });
});

//$(function () {
   
//});