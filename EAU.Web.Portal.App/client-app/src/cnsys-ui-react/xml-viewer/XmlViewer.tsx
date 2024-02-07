import { moduleContext } from 'cnsys-core';
import React from 'react';
import { ClosingTag, TagData } from './ClosingTag';
import { OpeningTagLink } from './OpeningTagLink';

export interface XmlViewerProps {
    expanded?: boolean;
    data: TagData;
}

interface XmlViewerState {
    expanded: boolean;
}

class XmlViewer extends React.Component<XmlViewerProps, XmlViewerState> {
    constructor(props?: XmlViewerProps) {
        super(props);

        //Init state
        this.state = this.props.data.expand != undefined ? { expanded: this.props.data.expand} : { expanded: this.props.expanded || false };

        this.shouldComponentUpdate = this.shouldComponentUpdate.bind(this);
        this.toggle = this.toggle.bind(this);
    }

    shouldComponentUpdate(nextProps: XmlViewerProps, nextState: XmlViewerState) {
        return this.state != nextState;
    }

    toggle() {
        this.setState({expanded: !this.state.expanded});
    }


    render() {
        let self = this;
        let children: any = null;

        if(this.props.data && this.props.data.nodes) {
            children = this.props.data.nodes.map(function (node: TagData, idx: number) {
                return (<XmlViewer data={node} expanded={self.props.expanded} key={idx} />);
            });
        }

        this.props.data.expand = this.state.expanded;

        return (
            <ul className="xml">
                <OpeningTagLink onLinkClick={this.toggle} data={this.props.data} />
                <div className={this.state.expanded != true ? 'hidden' : ''}>{children}</div>
                <ClosingTag data={this.props.data} />
            </ul>);
    }
}

export interface XmlViewerComponentProps {
    xmlString: string
}

export class XmlViewerComponent extends React.Component<XmlViewerComponentProps, any> {
    private content: any;

    constructor(props?: XmlViewerComponentProps) {
        super(props);

        let hasXmlParsingError = false;
        let xmlDOM: Document = undefined;
        try {
            //В IE при неправилен XML гърми грешка.
            xmlDOM = new DOMParser().parseFromString(props.xmlString, 'text/xml');
        } catch (e) {
            console.log(e);
            hasXmlParsingError = true;
        }

        //В нормалните браузъри при грешка в XML-а в документа xmlDOM в body-то му се появява таг parsererror с детайлна информация за грешката.
        if (!xmlDOM || xmlDOM.getElementsByTagName('parsererror').length > 0) {
            hasXmlParsingError = true;
        }

        if (hasXmlParsingError) {
            this.content = (<div className="alert alert-dismissable alert-danger">{moduleContext.errors.XmlParseError}</div>);
        } else {
            this.content = (<XmlViewer data={this.xmlToJson(xmlDOM)} expanded={true} />);
        }
    }

    render() {
        return (<div className="xml-container">{this.content}</div>); 
    }

    private loadAttributes(obj: any, attributes: NamedNodeMap) {
		let result: any = {};
		for (var j = 0; j < attributes.length; j++) {
			var attribute = attributes.item(j);
			result[attribute.nodeName] = attribute.nodeValue;
		}
				
		return result;
	}

    private xmlToJson(xml: any): any {
		var obj: any = {};
        if (xml.nodeType == 9) {
            //DOCUMENT_NODE
            if (xml.childNodes.length == 1) {
                //един Root елемент
				obj = this.xmlToJson(xml.childNodes.item(0));
			}
            else {
                //повече от един Root елемент
				obj["nodes"] = [];
						
				for(var i = 0; i < xml.childNodes.length; i++) {
					var item = xml.childNodes.item(i);
							
					obj["nodes"].push(this.xmlToJson(item));
				}
			}
				
		} else {
			if(xml.nodeType == 1) {
				//ELEMENT_NODE

                obj["name"] = xml.nodeName;

				if(xml.attributes && xml.attributes.length > 0) {
					obj["attrs"] = this.loadAttributes(obj, xml.attributes);
				}
						
				if (xml.hasChildNodes()) {
					if(xml.childNodes.length == 1 && xml.childNodes.item(0).nodeType == 3) {
						var item = xml.childNodes.item(0);
						obj["value"] = item.nodeValue;
					} else {
						obj["nodes"] = [];
						for(var i = 0; i < xml.childNodes.length; i++) {
							var item = xml.childNodes.item(i);
                            if (item.nodeType == 1) {
                                obj["nodes"].push(this.xmlToJson(item));
                            }
						}
					}
				}
			}
		}
				
		return obj;
	}
}