

$(document).ready(() => {

});

(function () {

    // Toggle side bar
    var sidebarAndWrapper = $("#sidebar,#wrapper");
    var icon = $("#sidebarToggle i.fa");
    var selt = this;
    $("#sidebarToggle").on("click", () => {
        console.log("Clicked")
        sidebarAndWrapper.toggleClass("hide-sidebar");
        if (sidebarAndWrapper.hasClass('hide-sidebar')){          
            icon.removeClass("fa-angle-left");
            icon.addClass("fa-angle-right");         
        }            
        else {         
            icon.removeClass("fa-angle-right");
            icon.addClass("fa-angle-left");         
        }
            
    });
    
})()

