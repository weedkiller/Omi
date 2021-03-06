﻿IMSChart = {
    ChartJSONData: new Object(),
    SwfRoot: "Content/FusionChartsSWF/",
    Render: function (chartObj, width, height) {
        var uniqueId = chartObj.containerId;
        var chartId = this.GetChartId(uniqueId);
        this.Dispose(uniqueId);
        var chartURL = this.SwfRoot + '' + chartObj.chartType.replace('Chart', '.swf');
        var chart = new FusionCharts(chartURL, chartId, width, height, "0", "0");
        if (chart) {
            chart.setDataXML(chartObj.xmlData);
            chart.render(uniqueId);
            this.SyncChartData(chart);
        }
    },
    SyncChartData: function (chart) {
        this.ChartJSONData[chart.id] = chart.getJSONData();
        for (var key in this.ChartJSONData) {
            if (this.ChartIsRemovable(key))
                this.ChartJSONData[key] = null;
        }
    },

    ChartIsRemovable: function (id) {
        if (FusionCharts.items[id])
            return false;
        return true;
    },

    GetChartJSONData: function (uniqueId) {
        return this.ChartJSONData[this.GetChartId(uniqueId)];
    },
    PartialUpdate: function (label, activeState, updateType, uniqueId) {
        var chart = this.GetChart(uniqueId);
        if (chart) {

            if (chart.chartType().substring(0, 3).toLowerCase() == "mss") {
                alert("stack chart not implemented yet");
            }
            else if (chart.chartType().substring(0, 2).toLowerCase() === "ms") {
                if (!!updateType && updateType == "Series") this.UpdateMSSeries(label, activeState, uniqueId);
                else this.UpdateMSCategory(label, activeState, uniqueId);
            }
            else if (chart.chartType().substring(0, 2).toLowerCase() == "ss") this.UpdateSS(label, activeState, uniqueId);
            chart.setJSONData(this.GetDisplayableData(uniqueId));
            chart.render(uniqueId);
        }
    },
    GetDisplayableData: function (uniqueId) {
        var chart = this.GetChart(uniqueId);
        if (chart) {
            var data = $.extend(true, {}, this.GetChartJSONData(uniqueId)); 
            if (chart.chartType().substring(0, 3).toLowerCase() == "mss") {
                return this.MSSRemoveFalse(data, uniqueId);
            }
            else if (chart.chartType().substring(0, 2).toLowerCase() == "ms") {
                return this.MSRemoveFalse(data, uniqueId);
            }

            else if (chart.chartType().substring(0, 2).toLowerCase() == "ss") {
                return this.SSRemoveFalse(data, uniqueId);
            }
        }
    },
    UpdateSS: function (label, activeState, uniqueId) {
        var chart = this.GetChart(uniqueId);
        if (chart) {
            var allData = this.GetChartJSONData(uniqueId);
            var dataArray = allData.data;
            var dataPosition = this.GetPosition(dataArray, label);
            if (!!dataPosition || dataPosition == 0) dataArray[dataPosition].active = activeState;
        }
    },
    GetPosition: function (dataArray, label) {
        var position;
        $.each(dataArray, function (index) {
            if (dataArray[index].label == label) { position = index; }
        });
        return position;
    },
    UpdateMSCategory: function (categoryName, activeState, uniqueId) {
        var chart = this.GetChart(uniqueId);
        if (chart) {
            var allData = this.GetChartJSONData(uniqueId);
            var categoryArray = allData.categories[0].category;

            var categoryPosition = this.GetPosition(categoryArray, categoryName);
            if (!!categoryPosition || categoryPosition == 0) {
                categoryArray[categoryPosition].active = activeState;
                var datasetArray = allData.dataset;
                $.each(datasetArray, function (index) {
                    datasetArray[index].data[categoryPosition].active = activeState;
                });
            }
        }
    },
    UpdateMSSeries: function (seriesName, activeState, uniqueId) {
        var chart = this.GetChart(uniqueId);
        if (chart) {
            var allData = this.GetChartJSONData(uniqueId);
            var datasetArray = allData.dataset;
            $.each(datasetArray, function (index) {
                if (datasetArray[index].seriesname == seriesName)
                    datasetArray[index].active = activeState;
            });
        }
    },
    MSRemoveFalse: function (allData) {
        if (!allData.categories) return;
        var categoryArray = allData.categories[0].category;
        categoryArray.removeItemByAttr("active", false);

        var datasetArray = allData.dataset;
        datasetArray.removeItemByAttr("active", false);
        $.each(datasetArray, function (index) {
            datasetArray[index].data.removeItemByAttr("active", false);
        });
        return allData;
    },
    SSRemoveFalse: function (allData) {
        var dataArray = allData.data;
        if (!dataArray) return;
        dataArray.removeItemByAttr("active", false);
        return allData;
    },
    GetJson: function (xmlData) {
        return FusionCharts.transcodeData(xmlData, 'xml', 'json');
    },
    GetChart: function (uniqueId) {
        if (FusionCharts.items) return FusionCharts.items[this.GetChartId(uniqueId)]; else null;
    },
    Dispose: function (uniqueId) {
        var chart = this.GetChart(uniqueId); if (!!chart) chart.dispose();
    },
    GetChartId: function (uniqueId) {
        return "obj_" + uniqueId;
    }

}

Array.prototype.removeItemByAttr = function (name, value) {
    var rest = $.grep(this, function (item) {
        return (item[name] !== value);
    });
    this.length = 0;
    this.push.apply(this, rest);
    return this;
};