if (!window.Festispec) {
	window.Festispec = {};
}
Festispec.storage = window.localStorage;

function getAllFormInputs() {
    return new Promise((resolve, error) => {
        let promises = [];
        let form = document.querySelector(".survey-container");
        let formElements = form.elements;
        for (let i = 0; i < formElements.length; i++) {
            let element = formElements[i];
            let type = element.type;
            if (type == "file") {
                let files = element.files;
                for (let j = 0; j < files.length; j++) {
                    let file = files[j];
                    promises.push(blobToBase64(element.name, file));
                }
            }
            else {
                if (element.type == "radio" && element.checked == true) {
                    promises.push(getInputValue(element));
                }
                else if (element.type != 'radio' && element.type != 'submit') {
                    promises.push(getInputValue(element));
                }
            }
        }
        Promise.all(promises).then((values) => {
            resolve(values);
        });
    });
}

function saveAllFormInputs() {
    getAllFormInputs().then((values) => {
        let uploadingCase = {};
        values.forEach((item, index, array) => {
            if (uploadingCase.hasOwnProperty(item.name)) {
                if (Array.isArray(item.value)) {
                    uploadingCase[item.name].push(item.value[0]);
                }
            }
            else {
                uploadingCase[item.name] = item.value;
            }
        });
        saveToLocalStorage(uploadingCase);
    });
}


async function getInputValue(inputElement) {
	return new Promise((resolve, error) => {
		resolve({
			value: inputElement.value,
			name: inputElement.name,
		});
	});
}

async function blobToBase64(inputName, blob) {
	return new Promise((resolve, error) => {
		let reader = new FileReader();
		reader.readAsDataURL(blob);
		reader.onloadend = function () {
			resolve({
				value: [reader.result],
				name: inputName,
			});
		}
	});
}

function getCasesFromLocalStorage() {
	let storageResult = Festispec.storage.getItem("survey");

	if (storageResult == null) {
		storageResult = {};
	}
	else {
		storageResult = JSON.parse(storageResult);
	}

	return storageResult;
}

function saveToLocalStorage(result) {
	let surveyCases = getCasesFromLocalStorage();

	if (surveyCases.hasOwnProperty(surveyId)) {
		let cases = surveyCases[surveyId];
		cases.push(result);
	}
	else {
		surveyCases[surveyId] = [];
		let cases = surveyCases[surveyId];
		cases.push(result);
	}
	Festispec.storage.setItem("survey", JSON.stringify(surveyCases));
}

setInterval(() => {
	let allCases = getCasesFromLocalStorage();
	for (surveyCaseKey in allCases) {
		let cases = allCases[surveyCaseKey];
		if (cases == null && cases.length <= 0) {
			continue;
		}
		for (var i = 0; i < cases.length; i++) {
			let surveyCase = cases[i];
			uploadSurveyCase(allCases, cases, i, surveyCase);
		}
	}
}, 5000);

function uploadSurveyCase(allCases, surveyCases, index, currentCase) {
	$.ajax({
		type: "POST",
		data: currentCase,
		success: function (data) {
			surveyCases.splice(index, 1);
			localStorage.setItem("survey", JSON.stringify(allCases));
		},
		error: function (errorMsg) {
			console.log("error");
		}
	});
}

function validateTableQuestions() {
    let tableQuestionsAreValid = true;
    for (let a = 0; a < tableQuestions.length; a++) {
        let tableQuestion = tableQuestions[a];
        if (tableQuestion.isValid() === false) {
            tableQuestionsAreValid = false;
            tableQuestion.displayNotCompleted();
        }
    }
    return tableQuestionsAreValid;
}

function validateImageQuestion() {
    let imageQuestionsAreValid = true;
    let imageUploads = document.querySelectorAll("input[type=file]");
    for (var i = 0; i < imageUploads.length; i++) {
        let image = imageUploads[i];
        let files = image.files;
        let maxNumberOfImages = image.getAttribute("max");
        if (files.length > maxNumberOfImages || files.length == 0) {
            let container = document.querySelector("input[type=file]").parentElement;
            let errorHolder = container.querySelector(".error");
            errorHolder.innerHTML = "Je mag maar " + maxNumberOfImages + " uploaden en je moet minimaal 1 afbeelding uploaden";
            imageQuestionsAreValid = false;
        }
    }
    return imageQuestionsAreValid;
}

function validateDrawQuestion() {
    let drawQuestionsAreValid = true;
    for (let a = 0; a < drawQuestions.length; a++) {
        let drawQuestion = drawQuestions[a];
        if (drawQuestion.isValid() === false) {
            drawQuestionsAreValid = false;
            drawQuestion.displayNotCompleted();
        }
    }
    return drawQuestionsAreValid;
}

function clearInputFields() {
    $(".survey-container").trigger("reset");
    for (let i = 0; i < drawQuestions.length; i++) {
        drawQuestions[i].clear();
    }
    for (let i = 0; i < tableQuestions.length; i++) {
        tableQuestions[i].clear();
    }
    let errorContainers = document.querySelectorAll(".error");
    for (let i = 0; i < errorContainers.length; i++) {
        errorContainers[i].innerHTML = "";
    }
}

$(document).ready(() => {
    $.extend(jQuery.validator.messages, {
        required: "Dit veld is verplicht",
    });
	$("#saveSurvey").click(() => {
        $(".survey-container").validate({
			ignore: "input[type='file']",
        });

        $(".survey-container").valid();
        validateTableQuestions();
        validateDrawQuestion();
        validateImageQuestion();

        if ($(".survey-container").valid() && validateTableQuestions() && validateDrawQuestion() && validateImageQuestion()) {
            saveAllFormInputs();
            clearInputFields();
            
			alert("Uw case is opgeslagen");
		}
		else {
			alert("Survey is niet compleet ingevuld");
		}
	});
});