if (!window.Festispec) {
	window.Festispec = {};
}
Festispec.storage = window.localStorage;

function saveAllFormInputs() {
	let promises = [];
	let form = document.querySelector("form");
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
		if (cases == null && cases.length < 0) {
			continue;
		}
		for (var i = 0; i < cases.length; i++) {
			let surveyCase = cases[i];
			uploadSurveyCase(allCases, cases, i, surveyCase);
		}
	}
	localStorage.setItem("survey", JSON.stringify(allCases));
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

$(document).ready(() => {
	$("#saveSurvey").click(() => {
		console.log("click");
		$("form").validate({
			ignore: "input[type='file']",
		});
		if ($("form").valid()) {
			saveAllFormInputs();
			$("form").trigger("reset");
			alert("Uw case is opgeslagen");
		}
		else {
			alert("Survey is niet compleet ingevuld");
		}
	});

});

$("form").submit((event) => {
	event.preventDefault();
	$("form").validate({
		ignore: "input[type='file']",
	});
	if ($("form").valid()) {
		saveAllFormInputs();
		$("form").trigger("reset");
		alert("Uw case is opgeslagen");
	}
});