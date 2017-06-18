// #region First code 

//function startup() {

//    var element = document.getElementById("username");
//    element.innerHTML = "Maria Paula Vilhena Mascarenhas";

//    var main = document.getElementById("main");

//    main.onmouseenter = () => {
//        main.style.background = "#888";
//    }
//    main.onmouseleave = () => {
//        main.style.background = "";
//    }
//}

//startup();

// #endregion

$(document).ready(() => {

});

(function () {

    //var element = $("#username");
    //element.text("Maria Paula Vilhena Mascarenhas");

    //var main = $("#main");
    //console.log(main)

    //main.on("mouseenter", function () {
    //    console.log("onmouseenter")
    //    main.css( "background-color","#888");
    //});
    //main.on("mouseleave", function () {
    //    console.log("onmouseleave")
    //    main.css("background-color","");
    //});
    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    var me = $(this)
    //    alert(me.text());
    //})
    
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

