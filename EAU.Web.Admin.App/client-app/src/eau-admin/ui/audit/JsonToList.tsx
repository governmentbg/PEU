import React from 'react';
import { BaseProps } from 'cnsys-ui-react';


interface JsonToListProps extends BaseProps {
	json: any
}

const JsonToList: React.FC<JsonToListProps> = ({json}) => {

	let jsonData =  JSON.parse(JSON.stringify(json))

	let result = [];
	
	Object.keys(jsonData).forEach(function(key) {
		
		if (typeof jsonData[key] === 'object')
			result.push(<li key={key}><span className="badge badge-secondary"><b>{key}</b>: <JsonToList key={key} json={jsonData[key]}/></span></li>);
		else
			result.push(<li key={key}><b>{key}</b>: {jsonData[key]}</li>);

	});

	return <>
		<ul className="tree-list-graph">{result}</ul>
	</>
}


export default JsonToList; 