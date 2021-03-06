﻿function drawChart(data) {
    var width = parseFloat(d3.select("div#chart-wrapper").style("width")) - 10,
      height = width,
      radius = width / 2,
      innerRadius = 0.3 * radius;

    var pie = d3.layout.pie()
        .sort(null)
        .value(function (d) { return d.width; });

    var tip = d3.tip()
      .attr('class', 'd3-tip')
      .offset([0, 0])
      .html(function (d) {
          if (d.data.totalTasks == 0) {
              return d.data.label + ": задач пока нет";
          }
          else{
              var tasks;
              if (d.data.doneTasks == 0) {
                  tasks = ": сделано <span style='color:#f00'>0 из " + d.data.totalTasks + "</span>";
              }
              else if (d.data.doneTasks == d.data.totalTasks) {
                  tasks = ": сделано <span style='color:#00ff21'>" + d.data.doneTasks + " из " + d.data.totalTasks + "</span>";
              }
              else {
                  tasks = ": сделано <span style='color:#ffd800'>" + d.data.doneTasks + " из " + d.data.totalTasks + "</span>";
              }
              return d.data.label + tasks;
          }
      });

    var arc = d3.svg.arc()
      .innerRadius(innerRadius)
      .outerRadius(function (d) {
          return (radius - innerRadius) * (d.data.score / 100.0) + innerRadius;
      });

    var outlineArc = d3.svg.arc()
            .innerRadius(innerRadius)
            .outerRadius(radius);

    var svg = d3.selectAll("svg").remove();

    svg = d3.select("div#chart-wrapper").append("svg")
            .attr("width", width)
            .attr("height", height)
            .append("g")
            .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    svg.call(tip);
   
    data.forEach(function (d) {
        d.id = d.id;
        //d.order = +d.order;
        d.color = d.color;
        d.weight = +d.weight;
        d.score = +d.score;
        d.width = +d.weight;
        d.label = d.label;
    });
    // for (var i = 0; i < data.score; i++) { console.log(data[i].id) }

    var path = svg.selectAll(".solidArc")
        .data(pie(data))
        .enter().append("path")
        .attr("data", function (d) { return d.data.label;})
        .attr("fill", function (d) { return d.data.color; })
        .attr("class", "solidArc")
        .attr("stroke", "gray")
        .attr("d", arc)
        .on('mouseover', tip.show)
        .on('mouseout', tip.hide)
        .on('click', function (d, i) { onChartClick(d.data.label); });

    var outerPath = svg.selectAll(".outlineArc")
        .data(pie(data))
        .enter().append("path")
        .attr("fill", "none")
        .attr("stroke", "gray")
        .attr("class", "outlineArc")
        .on('mouseover', tip.show)
        .on('mouseout', tip.hide)
        .attr("d", outlineArc);


    // calculate the weighted mean score
    //var score =
    //  data.reduce(function (a, b) {
    //      //console.log('a:' + a + ', b.score: ' + b.score + ', b.weight: ' + b.weight);
    //      return a + (b.score * b.weight);
    //  }, 0) /
    //  data.reduce(function (a, b) {
    //      return a + b.weight;
    //  }, 0);

    //svg.append("svg:text")
    //  .attr("class", "aster-score")
    //  .attr("dy", ".35em")
    //  .attr("text-anchor", "middle") // text-align: right
    //  .text("Ы");

    //d3.csv("https://gist.githubusercontent.com/bbest/2de0e25d4840c68f2db1/raw/52757de7e4584a6ff8cefbd2f8cea8a0d7cc2f0c/aster_data.csv", 
    //d3.csv("TextFile1.csv", function (error, data) {
    //    data.forEach(function (d) {
    //        d.id = d.id;
    //        d.order = +d.order;
    //        d.color = d.color;
    //        d.weight = +d.weight;
    //        d.score = +d.score;
    //        d.width = +d.weight;
    //        d.label = d.label;
    //    });
    //    // for (var i = 0; i < data.score; i++) { console.log(data[i].id) }
    //    var path = svg.selectAll(".solidArc")
    //        .data(pie(data))
    //        .enter().append("path")
    //        .attr("fill", function (d) { return d.data.color; })
    //        .attr("class", "solidArc")
    //        .attr("stroke", "gray")
    //        .attr("d", arc)
    //        .on('mouseover', tip.show)
    //        .on('mouseout', tip.hide)
    //        .on('click', function (d, i) { onChartClick(d.data.label);});
    //    var outerPath = svg.selectAll(".outlineArc")
    //        .data(pie(data))
    //        .enter().append("path")
    //        .attr("fill", "none")
    //        .attr("stroke", "gray")
    //        .attr("class", "outlineArc")
    //        .attr("d", outlineArc);
    //    // calculate the weighted mean score
    //    //var score =
    //    //  data.reduce(function (a, b) {
    //    //      //console.log('a:' + a + ', b.score: ' + b.score + ', b.weight: ' + b.weight);
    //    //      return a + (b.score * b.weight);
    //    //  }, 0) /
    //    //  data.reduce(function (a, b) {
    //    //      return a + b.weight;
    //    //  }, 0);
    //    //svg.append("svg:text")
    //    //  .attr("class", "aster-score")
    //    //  .attr("dy", ".35em")
    //    //  .attr("text-anchor", "middle") // text-align: right
    //    //  .text("Ы");
    //});
}