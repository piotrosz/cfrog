let addIngredientModal;

window.showAddIngredientBootstrapModal = () => {
    addIngredientModal = new bootstrap.Modal(document.getElementById('addIngredientModal'));
    addIngredientModal.show();
}

window.closeAddIngredientBootstrapModal = () => {
    if (addIngredientModal != undefined) {
        addIngredientModal.hide();
    }
}

let addStepModal;

window.showAddStepBootstrapModal = () => {
    addStepModal = new bootstrap.Modal(document.getElementById('addStepModal'));
    addStepModal.show();
}

window.closeAddStepBootstrapModal = () => {
    if (addStepModal != undefined) {
        addStepModal.hide();
    }
}