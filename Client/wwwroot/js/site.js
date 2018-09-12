// Write your JavaScript code.
$(document).ready(function () {
    names.push("Peter");
    names.push("Margarete");
    names.push("Suzanne");
    names.push("Anna");
    names.push("Linda");
    names.push("Paul");
    names.push("John");
    names.push("George");
    names.push("Martin");
    names.push("Keith");
    names.push("Jack");
    names.push("Elsa");
    names.push("Theresa");
    names.push("Elisabeth");
    localStorage.clear();
    if (document.getElementById("ApiUrl") !== null) {
        if (localStorage.getItem("ApiUrl") === null || localStorage.getItem("ApiUrl" !== document.getElementById("ApiUrl").innerHTML)) {
            localStorage.setItem("ApiUrl", document.getElementById("ApiUrl").innerHTML);
        }
        timerJob();
    }
    console.log('documentReady');
});
let names=[];
$(".uppercase").keyup(function () {
    var text = $(this).val();
    $(this).val(text.toUpperCase());
});

function clearErrors() {
    $(".validation-summary-errors").empty();
};

function timerJob() {
    const tenSeconds = 10000;
    const oneSecond = 1000;
    $.ajax({
        url: localStorage.getItem("ApiUrl") + "api/book",
        type: "GET",
        dataType: "json",
        success: function (books) {
            if (books.length === 0) {
                setTimeout(timerJob, oneSecond);
                console.log("No books found!");
                return;
            }
            const selectedItem = Math.floor(Math.random() * books.length);
            let selectedBook = books[selectedItem];
            if (selectedBook.disabled === true) {
                console.log(selectedBook.title + " is blocked for loan!");
                return;
            }
            selectedBook.inShelf = !selectedBook.inShelf;
            if (selectedBook.inShelf === false) {
                const selectedName = Math.floor(Math.random() * names.length);
                selectedBook.loanedTo = names[selectedName];
            }
            else {
                selectedBook.loanedTo = "";
            }
            $.ajax({
                url: localStorage.getItem("ApiUrl") + "api/book/" + selectedBook.id,
                contentType: "application/json",
                type: "PUT",
                data: JSON.stringify(selectedBook),
                dataType: "json"
            });

            const selector = `#${selectedBook.id} td:eq(3)`;
            const selector2 = `#${selectedBook.id + "_2"} td:eq(4)`;
            const selector3 = `#${selectedBook.id + "_3"}`;
            if (selectedBook.inShelf === true) {
                $(selector).text("Available");
                $(selector).removeClass("alert-danger");
                $(selector2).text("Available");
                $(selector2).removeClass("alert-danger");
                $(selector3).text("Available");
                $(selector3).removeClass("alert-danger");
                console.log(selectedBook.title + " is Available!");
            }
            else {
                $(selector).text("Loaned to: " + selectedBook.loanedTo);
                $(selector).addClass("alert-danger");
                $(selector2).text("Loaned to: " + selectedBook.loanedTo);
                $(selector2).addClass("alert-danger");
                $(selector3).text("Loaned to: " + selectedBook.loanedTo);
                $(selector3).addClass("alert-danger");
                console.log(selectedBook.title + " is loaned to " + selectedBook.loanedTo + "!");
            }
            if (document.getElementById("All") !== null) {
                doFiltering();
            }
        }
    });
    setTimeout(timerJob, tenSeconds);
}

function doFiltering() {
    let selection = 0;
    let radiobtn = document.getElementById("All");
    if (radiobtn.checked === false) {
        radiobtn = document.getElementById("InShelf");
        if (radiobtn.checked === true) {
            selection = 1;
        }
        else {
            selection = 2;
        }
    }

    var table = $('#books > tbody');
    $('tr', table).each(function () {
        $(this).removeClass("hidden");
        let td = $('td:eq(3)', $(this)).html();
        if (td !== undefined) {
            td = td.trim();
        }
        if (td !== "Available" && selection === 1) {
            $(this).addClass("hidden");  //Show only InShelf
        }
        if (td === "Available" && selection === 2) {
            $(this).addClass("hidden"); //Show only NotInShelf
        }
    });
};

function showModals() {
    window.open("./html/BookOverview.html", "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=50,width=500,height=400");
}
