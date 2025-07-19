(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Sidebar Toggler
    $('.sidebar-toggler').click(function () {
        $('.sidebar, .content').toggleClass("open");
        return false;
    });


    // Progress Bar
    $('.pg-bar').waypoint(function () {
        $('.progress .progress-bar').each(function () {
            $(this).css("width", $(this).attr("aria-valuenow") + '%');
        });
    }, {offset: '80%'});


    // Calender
    $('#calender').datetimepicker({
        inline: true,
        format: 'L'
    });


    // Testimonials carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        items: 1,
        dots: true,
        loop: true,
        nav : false
    });


    //// === DYNAMIC CHART ===
    //fetch('/Admin/counts')
    //    .then(response => response.json())
    //    .then(data => {
    //        var ctx = $("#worldwide-sales").get(0).getContext("2d");
    //        new Chart(ctx, {
    //            type: "bar",
    //            data: {
    //                labels: ["Resumes", "Portfolios"],
    //                datasets: [{
    //                    label: "Total Generated",
    //                    data: [data.resumeCount, data.portfolioCount],
    //                    backgroundColor: [
    //                        "rgba(0, 156, 255, 0.7)",
    //                        "rgba(0, 156, 255, 0.5)"
    //                    ],
    //                    borderColor: [
    //                        "rgba(0, 156, 255, 1)",
    //                        "rgba(0, 156, 255, 1)"
    //                    ],
    //                    borderWidth: 1
    //                }]
    //            },
    //            options: {
    //                responsive: true,
    //                scales: {
    //                    y: {
    //                        beginAtZero: true,
    //                        ticks: {
    //                            stepSize: 1
    //                        }
    //                    }
    //                },
    //                plugins: {
    //                    legend: {
    //                        display: true,
    //                        position: "top"
    //                    }
    //                }
    //            }
    //        });
    //    })
    //    .catch(error => {
    //        console.error("Chart data fetch error:", error);
    //    });



    fetch('/Admin/counts-per-day')
        .then(response => response.json())
        .then(data => {
            console.log("Chart Data:", data); // ? Use this to verify in browser console

            var ctx = $("#worldwide-sales").get(0).getContext("2d");
            new Chart(ctx, {
                type: "bar",
                data: {
                    labels: data.dates, // ? fixed
                    datasets: [
                        {
                            label: "Resumes",
                            data: data.resumeCounts, // ? fixed
                            backgroundColor: "rgba(0, 156, 255, 0.7)",
                            borderColor: "rgba(0, 156, 255, 1)",
                            borderWidth: 1
                        },
                        {
                            label: "Portfolios",
                            data: data.portfolioCounts, // ? fixed
                            backgroundColor: "rgba(255, 99, 132, 0.7)",
                            borderColor: "rgba(255, 99, 132, 1)",
                            borderWidth: 1
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                stepSize: 1
                            }
                        }
                    },
                    plugins: {
                        legend: {
                            display: true,
                            position: "top"
                        }
                    }
                }
            });
        })
        .catch(error => {
            console.error("Chart data fetch error:", error);
        });






    // portfolio resume Chart
    fetch('/Admin/counts-per-day')
        .then(response => response.json())
        .then(data => {
            console.log("Chart Data:", data);

            var ctx = document.getElementById("Resume-Portfolio2").getContext("2d");
            new Chart(ctx, {
                type: "line",
                data: {
                    labels: data.dates,
                    datasets: [
                        {
                            label: "Resumes",
                            data: data.resumeCounts,
                            borderColor: "rgba(0, 156, 255, 1)",
                            backgroundColor: "rgba(0, 156, 255, 0.3)",
                            fill: true,
                            tension: 0.3
                        },
                        {
                            label: "Portfolios",
                            data: data.portfolioCounts,
                            borderColor: "rgba(255, 99, 132, 1)",
                            backgroundColor: "rgba(255, 99, 132, 0.3)",
                            fill: true,
                            tension: 0.3
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true,
                            ticks: {
                                stepSize: 1
                            }
                        }
                    },
                    plugins: {
                        legend: {
                            display: true,
                            position: "top"
                        }
                    }
                }
            });
        })
        .catch(error => {
            console.error("Chart data fetch error:", error);
        });


    //Total resuems & portfolios 
    fetch('/Admin/counts')
        .then(response => response.json())
        .then(data => {
            console.log("Fetched Counts:", data); // Debug
            document.getElementById("total-resumes").textContent = data.resumeCount;
            document.getElementById("total-portfolios").textContent = data.portfolioCount;
            document.getElementById("total-users").textContent = data.userCount;
        })
        .catch(error => {
            console.error("Error fetching counts:", error);
        });



    // Single Line Chart
    var ctx3 = $("#line-chart").get(0).getContext("2d");
    var myChart3 = new Chart(ctx3, {
        type: "line",
        data: {
            labels: [50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150],
            datasets: [{
                label: "Salse",
                fill: false,
                backgroundColor: "rgba(0, 156, 255, .3)",
                data: [7, 8, 8, 9, 9, 9, 10, 11, 14, 14, 15]
            }]
        },
        options: {
            responsive: true
        }
    });


    // Single Bar Chart
    var ctx4 = $("#bar-chart").get(0).getContext("2d");
    var myChart4 = new Chart(ctx4, {
        type: "bar",
        data: {
            labels: ["Italy", "France", "Spain", "USA", "Argentina"],
            datasets: [{
                backgroundColor: [
                    "rgba(0, 156, 255, .7)",
                    "rgba(0, 156, 255, .6)",
                    "rgba(0, 156, 255, .5)",
                    "rgba(0, 156, 255, .4)",
                    "rgba(0, 156, 255, .3)"
                ],
                data: [55, 49, 44, 24, 15]
            }]
        },
        options: {
            responsive: true
        }
    });


    // Pie Chart
    var ctx5 = $("#pie-chart").get(0).getContext("2d");
    var myChart5 = new Chart(ctx5, {
        type: "pie",
        data: {
            labels: ["Italy", "France", "Spain", "USA", "Argentina"],
            datasets: [{
                backgroundColor: [
                    "rgba(0, 156, 255, .7)",
                    "rgba(0, 156, 255, .6)",
                    "rgba(0, 156, 255, .5)",
                    "rgba(0, 156, 255, .4)",
                    "rgba(0, 156, 255, .3)"
                ],
                data: [55, 49, 44, 24, 15]
            }]
        },
        options: {
            responsive: true
        }
    });


    // Doughnut Chart
    var ctx6 = $("#doughnut-chart").get(0).getContext("2d");
    var myChart6 = new Chart(ctx6, {
        type: "doughnut",
        data: {
            labels: ["Italy", "France", "Spain", "USA", "Argentina"],
            datasets: [{
                backgroundColor: [
                    "rgba(0, 156, 255, .7)",
                    "rgba(0, 156, 255, .6)",
                    "rgba(0, 156, 255, .5)",
                    "rgba(0, 156, 255, .4)",
                    "rgba(0, 156, 255, .3)"
                ],
                data: [55, 49, 44, 24, 15]
            }]
        },
        options: {
            responsive: true
        }
    });

    
})(jQuery);

