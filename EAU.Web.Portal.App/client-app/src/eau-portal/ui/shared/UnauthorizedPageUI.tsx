import  React from "react";
import Footer from "./Footer";
import { Header } from "./Header";

export function UnauthorizedPageUI() {

    return (<div id="react-layout">
        <Header />
        <div id="main-wrapper">
            <div id="content-wrapper">
                <div id="page-wrapper">
                    {/*
                    <div className="container-fluid">
                        <div className="card">
                            <div className="card-body">
                                <NotificationPanel notificationType={NotificationType.Warning} text={"Нямате права за достъп до избраната функционалност."} />
                            </div>
                        </div>
                    </div>
                    */}
                </div>
            </div>
        </div>
        <Footer />
    </div>)
}