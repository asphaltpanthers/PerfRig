function displayGraph(timeUri) {
    var container = document.getElementById('visualization');

    $(document).ready(function () {
        $.getJSON(timeUri)
            .done(function (data) {
                var items = [];
                $.each(data, function (key, item) {
                    items.push({ id: item.Id, x: item.TimeStamp, y: item.ElapsedTime });
                })

                var dataset = new vis.DataSet(items);

                function compare(a, b) {
                    if (a.TimeStamp < b.TimeStamp)
                        return -1;
                    if (a.TimeStamp > b.TimeStamp)
                        return 1;
                    return 0;
                }

                items.sort(compare);
                var options = {
                    style: 'bar',
                    barChart: { width: 50, align: 'center', handleOverlap: 'sideBySide' }, // align: left, center, right
                    drawPoints: false,
                    dataAxis: {
                        icons: true
                    },
                    orientation: 'top',
                    start: items[0].TimeStamp,
                    end: items[items.length - 1].TimeStamp
                };

                var graph2d = new vis.Graph2d(container, items, options);
            })
    })
}