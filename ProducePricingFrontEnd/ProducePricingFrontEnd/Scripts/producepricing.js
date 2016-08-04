$(document).ready(function () {
    var intSearchNum = 0;

    $('#btnClear').hide();

    $('#btnClear').click(function (e) {
        e.preventDefault();
        $("#tblMarketData tbody").empty();
        intSearchNum = 0;
        $('#btnClear').fadeOut();
        $('#txtProduct').val('');
    });

    $("#divMarketData").hide();
    $('#txtProduct').on('input', function (e) {
        if ($("#Markets").val() == "[Select Market]") {
            $("#status").html("<div class='row fade in'><div class='col-md-3'><p class='bg-danger text-center'>Select a market before searching.</p></div></div>");
        }
        else {
            $("#status").html("");
        }
    });



    $("#Markets").change(function () {


        $("#txtProduct").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    url: "/Home/GetProduceItems",
                    data: {
                        prod_term: request.term,
                        market_name: $("#Markets").val()
                    },
                    success: response,
                    dataType: 'json'
                });
            },
            minLength: 3,
            select: function (event, ui) {

                $.ajax({
                    type: "POST",
                    url: "/Home/MarketDataUserSelection",
                    data: {
                        commName: ui.item.commName,
                        packageDesc: ui.item.packageDesc,
                        itemSizeDesc: ui.item.itemSizeDesc,
                        varietyName: ui.item.varietyName,
                        market_name: $("#Markets").val()
                    },
                    success: function (data) {

                        $.ajax({
                            type: "POST",
                            url: "/Home/GetUserSearch",
                            data: {
                                commName: data.commName,
                                packageDesc: data.packageDesc,
                                itemSizeDesc: data.itemSizeDesc,
                                varietyName: data.varietyName,
                                marketName: $("#Markets").val()
                            },
                            dataType: 'json'
                        });

                        $("#divMarketData").fadeIn();
                        intSearchNum++;
                        $("#tblMarketData  tbody")
                            .append("<tr>"
                                    + "<td>" + intSearchNum + "</td>"
                                    + "<td>" + $("#Markets").val() + "</td>"
                                    + "<td> $" + data.highPriceMax.toFixed(2) + "</td>"
                                    + "<td> $" + data.lowPriceMin.toFixed(2) + "</td>"
                                    + "<td> $" + data.lastWeekAvg + "</td>"
                                    + "<td>" + data.commName + "</td>"
                                    + "<td>" + data.packageDesc + "</td>"
                                    + "<td>" + data.varietyName + "</td>"
                                    + "<td>" + data.itemSizeDesc + "</td>"
                                    + "<td>" + data.reportDate + "</td>"
                                    + "</tr>"
                                    );

                        $('#btnClear').show();
                    },
                    dataType: 'json'
                });
            }/*,
                    messages: {
                        noResults: "<p class='bg-warning text-center'>No results found. Refine your search query or select a different market.</p>",
                        results: function () { }
                    },*/
        });

        if ($("#Markets").val() != "[Select Market]") {
            $("#status").html("<div class='row fade in'><div class='col-md-3'><p class='bg-success text-center'>Market set, select a product.</p></div></div>");
        }
    });
});