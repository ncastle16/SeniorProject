var myurl = "https://cors-anywhere.herokuapp.com/https://api.yelp.com/v3/businesses/search?term=Grain-Station&location=220-Pacific-Ave-S-Monmouth-OR-97361&limit=1";

$.ajax({

    url: myurl,

    headers: {

        'Authorization': 'Bearer 3glYwaLZjmtLvAcgvmia-ocJ1tdhu6PAFCo0jCYrmsgHXZXX0tduCis8dKk3GMGO7Oc9jYYRLTPRSaWopVeUJMI8pjCj2nNcjDhh1mcYsMA3xjkndOqPba6k3_dOXnYx',

    },

    method: 'GET',

    dataType: 'json',

    success: function (data) {



        var totalresults = data.total;



        if (totalresults > 0) {



            $.each(data.businesses, function (i, item) {



                var id = item.id;

                var alias = item.alias;

                var phone = item.display_phone;

                var image = item.image_url;

                var name = item.name;

                var rating = item.rating;

                var reviewcount = item.review_count;

                var address = item.location.address1;

                var city = item.location.city;

                var state = item.location.state;

                var zipcode = item.location.zip_code;

                var price = item.price;



                $('#results').append('<div id="' + id + '" style="margin-top:50px;margin-bottom:50px;"><img src="' + image + '" style="width:200px;height:150px;"><br> <b>' + name + '</b> (' + alias + ')<br>Business ID: ' + id + '<br> Price: ' + price + '<br> Located at: ' + address + ' ' + city + ', ' + state + ' ' + zipcode + '<br>The phone number for this business is: ' + phone + '<br>This business has a rating of ' + rating + ' with ' + reviewcount + ' reviews.</div>');

            });

        } else {



            $('#results').append('<h5>We discovered no results!</h5>');

        }

    }

});