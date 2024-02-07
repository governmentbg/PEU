import { resourceManager } from "eau-core";
import React from "react";
import { Alert } from "reactstrap";
import Footer from "./Footer";
import { Header } from "./Header";

interface UnauthorizedPageProps {
    withLayout?: boolean;
}

export const UnauthorizedPageUI: React.FC<UnauthorizedPageProps> = (props) => {

    const content = <div className="card">
        <div className="card-body">
            <Alert color="warning">{resourceManager.getResourceByKey("GL_UNAUTHORIZED_USER_I")}</Alert>
        </div>
    </div>

    if (props.withLayout === true) {

        return <>
            <Header hideIconMenu />
            <div id="content-wrapper" className="content-wrapper">
                <div className="main-wrapper">
                    <div className="page-wrapper">
                        <div className="container-fluid">
                            {content}
                        </div>
                    </div>
                </div>
            </div>
            <Footer />
        </>
    }

    return content;
}