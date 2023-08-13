"use client";

import * as d3 from "d3";
import { useEffect } from "react";

const Graph = () => {
  useEffect(() => {
    // do not recreate graph if it already exists
    if (d3.select("#my_dataviz").node()) {
      return;
    }

    // set the dimensions and margins of the graph
    const margin = { top: 10, right: 30, bottom: 30, left: 60 },
      width = 460 - margin.left - margin.right,
      height = 400 - margin.top - margin.bottom;

    // append the svg object to the body of the page
    const svg = d3
      .select("#my_dataviz")
      .append("svg")
      .attr("width", width + margin.left + margin.right)
      .attr("height", height + margin.top + margin.bottom)
      .append("g")
      .attr("transform", `translate(${margin.left},${margin.top})`);

    //Read the data
    d3.csv(
      "https://raw.githubusercontent.com/holtzy/D3-graph-gallery/master/DATA/connectedscatter.csv",
      // When reading the csv, I must format variables:
      function (d) {
        if (!d.date || !d.value) {
          throw new Error("Date is missing");
        }
        const date = d3.timeParse("%Y-%m-%d")(d.date);
        if (!date) {
          throw new Error("Date is missing");
        }

        return { date, value: d.value };
      }
    ).then(
      // Now I can use this dataset:
      function (data) {
        // Add X axis --> it is a date format
        const x = d3
          .scaleTime()
          .domain(d3.extent(data, (d) => d.date) as Iterable<Date>)
          .range([0, width]);

        x(new Date());
        svg
          .append("g")
          .attr("transform", "translate(0," + height + ")")
          .call(d3.axisBottom(x));
        // Add Y axis
        const y = d3.scaleLinear().domain([8000, 9200]).range([height, 0]);
        svg.append("g").call(d3.axisLeft(y));
        // Add the line

        svg
          .append("path")
          .datum(data)
          .attr("fill", "none")
          .attr("stroke", "#69b3a2")
          .attr("stroke-width", 1.5)
          .attr(
            "d",
            d3
              .line<{ date: Date; value: string }>()
              .x((d) => x(d.date))
              .y((d) => y(parseInt(d.value)))
          );
        // Add the points
        svg
          .append("g")
          .selectAll("dot")
          .data(data)
          .join("circle")
          .attr("cx", (d) => x(d.date))
          .attr("cy", (d) => y(parseInt(d.value)))
          .attr("r", 5)
          .attr("fill", "#69b3a2");
      }
    );
  });

  return <div id="my_dataviz"></div>;
};

export default Graph;
