import { ObjectHelper } from 'cnsys-core';
import { Nomenclatures } from '../cache/Nomenclatures';
import { appConfig } from './ApplicationConfig';

export namespace ContextInfoHelper {
	export function getWebHelpUrl(fieldName: string = null): Promise<string> {
		let uri: string = window.location.href;
		let keyConfig: any[] = JSON.parse(appConfig.webHelpConfig);
		let webHelpKey: string = '';
		let matches: (RegExpExecArray | null) = null;

		for (var i = 0; i < keyConfig.length; i++) {
			let obj = keyConfig[i];
			let regExp = new RegExp(obj.pattern, 'i')

			matches = regExp.exec(uri);

			if (matches && matches.length > 0) {
				if (ObjectHelper.isStringNullOrEmpty(fieldName)) {
					webHelpKey = obj.key;
				} else {
					webHelpKey = `${obj.key}_${fieldName}`;
				}

				break;
			}
		}

		if (matches && matches[1]) {
			let srvId = matches[1];
			return Nomenclatures.getServices(s => s.serviceID == Number(srvId)).then(srvs => {
				if (srvs && srvs.length == 1) {
					webHelpKey = webHelpKey.replace("$1", srvs[0].sunauServiceUri);
					return createUrl(webHelpKey);
				}
				else
					throw new Error('Context help can not find service by id.');
			});
		}

		return Promise.resolve(createUrl(webHelpKey));
	};

	function createUrl(key: string = null): string {
		let webHelpUrl = appConfig.webHelpUrl;

		if (!ObjectHelper.isStringNullOrEmpty(key)) {
			key = key.replace(/-|\//g, '_');
			key = key.replace(/^_|_$/, '');

			webHelpUrl = `${webHelpUrl}?id=${key}`;
		}

		return webHelpUrl;
	}
}