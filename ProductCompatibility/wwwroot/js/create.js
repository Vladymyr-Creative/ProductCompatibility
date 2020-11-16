$("#reset").on("click", function (e) {
    e.preventDefault();
    reset();
});

$("#Img").on("change", function (e) {
    var img = document.getElementById("ProductImage");
    img.src = "";
});


$("form#productForm").submit(function (e) {
    e.preventDefault();
    const form = document.forms["productForm"];
    const id = form.elements["id"].value;

    var data = new FormData();

    var files = document.getElementById('Img').files;
    if (files.length > 0) {
        data.append("Img", files[0]);
    }
    data.append("Name", form.elements["name"].value);
    data.append("Desc", form.elements["desc"].value);
    data.append("Cat", form.elements["cat"].value);

    if (id == 0) {
        CreateProduct(data);
    } else {
        data.append("Id", id);
        EditProduct(data);
    }
});

function CheckProduct() {
    const form = document.forms["productForm"];
    const id = form.elements["id"].value;
    if (id != 0) GetProduct(id);
}

async function GetProducts() {

    const response = await fetch("/api/products", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const products = await response.json();
        let rows = document.querySelector("tbody");
        products.forEach(product => {
            rows.append(row(product));
        });
    }
}

async function GetProduct(id) {
    const response = await fetch("/api/products/" + id, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const product = await response.json();
        const form = document.forms["productForm"];
        form.elements["id"].value = product.id;
        form.elements["name"].value = product.name;
        form.elements["cat"].value = product.categoryId;
        form.elements["desc"].value = product.description;
        document.getElementById("ProductImage").setAttribute("src", "/img/"+product.img);        
    }
}

async function CreateProduct(data) {
    $.ajax({
        type: 'POST',
        url: 'Create',
        contentType: false,
        processData: false,
        data: data,
        success: function (response) {            
            reset();
            document.querySelector("tbody").append(row(response));
        },
        error: function (response, status, p3) {            
            HandleError(response.responseJSON);
        }
    });
}

async function HandleError(errorData) {
    if (errorData) {
        resetError();
        for (var prop in errorData) {
            addError(errorData[prop]);
        }
    }
    document.getElementById("errors").style.display = "block";
}

async function EditProduct(data) {
    $.ajax({
        type: 'POST',
        url: 'Edit/'+ data.Id,
        contentType: false,
        processData: false,
        data: data,
        success: function (response) {
            reset();                        
            document.querySelector("tr[data-rowid='" + response.id + "']").replaceWith(row(response));
        },
        error: function (response, status, p3) {
            HandleError(response.responseJSON);
        }
    });
}

async function DeleteProduct(id) {
    $.ajax({
        type: 'DELETE',
        url: 'Delete/' + id,
        contentType: false,
        processData: false,        
        success: function (response) {
            document.querySelector("tr[data-rowid='" + id + "']").remove();
        },
        error: function (response, status, p3) {
            HandleError(response.responseJSON);
        }
    });
}

function resetError() {
    document.getElementById("errors").innerHTML = '';
    document.getElementById("errors").style.display = "none";
}

function reset() {
    const form = document.forms["productForm"];
    form.reset();
    resetError();
    form.elements["id"].value = 0;
}

function addError(errors) {
    errors.forEach(error => {
        const p = document.createElement("p");
        p.append(error);
        document.getElementById("errors").append(p);
    });
}

var timer;
function scrollToTop() {
    var top = Math.max(document.body.scrollTop, document.documentElement.scrollTop);
    if (top > 0) {
        window.scrollBy(0, -50);
        timer = setTimeout('scrollToTop()', 20);
    } else clearTimeout(timer);
    return false;
}

function row(product) {

    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", product.id);

    const nameTd = document.createElement("td");
    const prodLink = document.createElement("a");
    prodLink.href = "/Product/Single/" + product.id;
    prodLink.append(product.name);
    nameTd.append(prodLink);
    tr.append(nameTd);    

    const catTd = document.createElement("td");
    const catLink = document.createElement("a");
    catLink.href = "/Product/List/" + product.category.name;
    catLink.append(product.category.name);
    catTd.append(catLink);
    tr.append(catTd);

    const imgTd = document.createElement("td");
    const img = document.createElement("img");
    img.src = "/img/" + product.img;
    img.classList.add("image-small-size");
    imgTd.append(img);
    tr.append(imgTd);

    const editTd = document.createElement("td");
    const editLink = document.createElement("a");
    editLink.setAttribute("data-id", product.id);
    editLink.setAttribute("class", "btn btn-primary");
    editLink.append("Edit");
    editLink.addEventListener("click", e => {
        e.preventDefault();
        scrollToTop();
        GetProduct(product.id);
    });
    editTd.append(editLink);
    tr.appendChild(editTd);

    const removeTd = document.createElement("td");
    const removeLink = document.createElement("a");
    removeLink.setAttribute("data-id", product.id);
    removeLink.setAttribute("class", "btn btn-secondary");
    removeLink.append("Delete");
    removeLink.addEventListener("click", e => {
        e.preventDefault();
        DeleteProduct(product.id);
    });
    removeTd.append(removeLink);
    tr.appendChild(removeTd);

    return tr;
}

CheckProduct();
GetProducts();
