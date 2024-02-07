import * as React from "react";
import { IDocumentFormManager, IApplicationFormManager } from "../../";

//Fix на проблем Type '{ registerAsyncOperation: <P>(promise: Promise<P>, forceShowLoadingUI?: boolean) => void; asyncErrors: (ApiError | ClientError | Error)[]; ... 9 more ...; fullHtmlName?: string; }' is not assignable to type 'LibraryManagedAttributes<C, Readonly<any> & Readonly<{ children?: ReactNode; }>>'.	C:\Projects\github\EAU\EAU.Web.Portal.App\ClientApp (tsconfig or jsconfig project)	C:\Projects\github\EAU\EAU.Web.Portal.App\ClientApp\src\Cnsys.UI.React\AsyncFrameUI.tsx	226	Active
type P = React.ComponentProps<any>;
type IntrinsicProps = JSX.IntrinsicAttributes &
    JSX.LibraryManagedAttributes<any, { children?: React.ReactNode }>;
type WrappedProps = P & IntrinsicProps & {};

const DocumentFormManagerContext = React.createContext(null);

export interface DocumentFormManagerProps {
    documentFormManager?: IDocumentFormManager;
}

export interface ApplicationFormManagerProps {
    documentFormManager?: IApplicationFormManager;
}

export function DocumentFormManagerProvider(props: Readonly<{ children: React.ReactNode }> & DocumentFormManagerProps): JSX.Element {
    return (
        <DocumentFormManagerContext.Provider value={props.documentFormManager}>
            {props.children}
        </DocumentFormManagerContext.Provider>
    )
}

export function withDocumentFormManager<C extends React.ComponentClass<DocumentFormManagerProps>>(Component: C, showError: boolean = true): C {

    class Wrapper extends React.Component<DocumentFormManagerProps, any> {

        constructor(props: DocumentFormManagerProps) {
            super(props);
        }

        render() {
            return (
                <DocumentFormManagerContext.Consumer>
                    { documentFormManager => (
                        <Component {...((this.props as unknown) as WrappedProps)} documentFormManager={documentFormManager}>{this.props.children}</Component>
                    )
                    }
                </DocumentFormManagerContext.Consumer>
            );
        }
    };

    return Wrapper as any;
}