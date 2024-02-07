import footerlogoEn from 'assets/images/footer-logo-eu-en.svg';
import footerlogoEu from 'assets/images/footer-logo-eu.svg';
import footerlogoOpduEn from 'assets/images/footer-logo-opdu-en.svg';
import footerlogoOpdu from 'assets/images/footer-logo-opdu.svg';
import { appConfig } from "cnsys-core";
import { AsyncUIProps, withAsyncFrame, BaseRoutePropsExt } from 'cnsys-ui-react';
import { CmsDataService, Constants as CoreConstants, Page, resourceManager, eauAuthenticationService } from 'eau-core';
import * as JsCookies from "js-cookie";
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Constants } from '../../Constants';
import { HelpUI } from '../common/HelpUI';
import { useHistory } from "react-router-dom";

interface FooterProps extends AsyncUIProps, BaseRoutePropsExt {
}

const Footer: React.FC<FooterProps> = (props) => {
    const history = useHistory();
    let footerLogoSelected:string;
    let footerLogoOpduSelected:string;
    const [cmsPages, loadCMSPages] = useState<Page[]>();
    const [isLoaded, setIsLoaded] = useState<boolean>(false);

    useEffect(() => {
        props.registerAsyncOperation(new CmsDataService().getPages().then(pages => {
            if (pages && pages.length > 0)
                loadCMSPages(pages);

            setIsLoaded(true)
        }))
    }, [])

    return (
        <div className="footer-wrapper">
            <footer className="footer">
                <div className="footer-containter-bg">
                    <div className="footer-links-container fixed-content-width">
                        {
                            isLoaded
                                ? <>
                                    <ul>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.News}>{cmsPageTitleMapper("NEWS")}</Link>
                                        </li>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.TestSign}>{resourceManager.getResourceByKey("GL_TEST_SIGN_L")}</Link>
                                        </li>
                                        <li className="footer-link">
                                            <Link to={CoreConstants.PATHS.DocumentPreview}>{resourceManager.getResourceByKey("GL_REVIEW_EDOCUMENTS_L")}</Link>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.PrivacyPolicy}>{cmsPageTitleMapper("PRIVACY_POLICY")}</Link>
                                        </li>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.AccessibilityPolicy}>{cmsPageTitleMapper("ACCESSIBILITY_POLICY")}</Link>
                                        </li>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.SecurityPolicy}>{cmsPageTitleMapper("SECURITY_POLICY")}</Link>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.TermOfUse}>{cmsPageTitleMapper("TERM_OF_USE")}</Link>
                                        </li>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.Cookies}>{cmsPageTitleMapper("COOKIES")}</Link>
                                        </li>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.SiteMap}>{resourceManager.getResourceByKey("GL_SITE_MAP_L")}</Link>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li className="footer-link">
                                            <HelpUI cssClass="btn btn-link" />
                                        </li>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.VideoLessons}>{cmsPageTitleMapper("VIDEO_LESSONS")}</Link>
                                        </li>
                                        <li className="footer-link">
                                            <Link to={Constants.PATHS.Contacts}>{cmsPageTitleMapper("CONTACTS")}</Link>
                                        </li>
                                    </ul>
                                    <button accessKey="0" tabIndex={-1} type="button" className="btn btn-link skip-to-content" title={resourceManager.getResourceByKey("GL_FAST_ACCESS_I")} onClick={() => history.push(Constants.PATHS.AccessFunction, null)}>{"0 - " + resourceManager.getResourceByKey(Constants.RESOURCES.AccessFunction)}</button>
                                    <HelpUI cssClass="btn btn-link skip-to-content" accessKey="1" />
                                    <button accessKey="2" tabIndex={-1} type="button" className="btn btn-link skip-to-content" title={resourceManager.getResourceByKey("GL_FAST_ACCESS_I")} onClick={() => eauAuthenticationService.userLogin()}>{"2 - " + resourceManager.getResourceByKey(Constants.RESOURCES.SignIn)}</button>
                                    <button accessKey="3" tabIndex={-1} type="button" className="btn btn-link skip-to-content" title={resourceManager.getResourceByKey("GL_FAST_ACCESS_I")} onClick={() => history.push(Constants.PATHS.Services, null)}>{"3 - " + resourceManager.getResourceByKey(Constants.RESOURCES.Services)}</button>
                                    <button accessKey="4" tabIndex={-1} type="button" className="btn btn-link skip-to-content" title={resourceManager.getResourceByKey("GL_FAST_ACCESS_I")} onClick={() => history.push(Constants.PATHS.MyEServices, null)}>{"4 - " + resourceManager.getResourceByKey(Constants.RESOURCES.MyEServices)}</button>
                                    <button accessKey="5" tabIndex={-1} type="button" className="btn btn-link skip-to-content" title={resourceManager.getResourceByKey("GL_FAST_ACCESS_I")} onClick={() => history.push(Constants.PATHS.Contacts, null)}>{"5 - " + resourceManager.getResourceByKey(Constants.RESOURCES.Contacts)}</button>
                                    <button accessKey="6" tabIndex={-1} type="button" className="btn btn-link skip-to-content" title={resourceManager.getResourceByKey("GL_FAST_ACCESS_I")} onClick={() => history.push(Constants.PATHS.PrivacyPolicy, null)}>{"6 - " + resourceManager.getResourceByKey(Constants.RESOURCES.PrivacyPolicy)}</button>
                                    <button accessKey="7" tabIndex={-1} type="button" className="btn btn-link skip-to-content" title={resourceManager.getResourceByKey("GL_FAST_ACCESS_I")} onClick={() => history.push(Constants.PATHS.SiteMap, null)}>{"7 - " + resourceManager.getResourceByKey(Constants.RESOURCES.SiteMap)}</button>  

                                    <div className="fixed-content-width">
                                        <div className="software-version">{resourceManager.getResourceByKey("GL_VERSION_L")}: {appConfig.version}</div>
                                    </div>
                                </>
                                : null
                        }
                    </div>
                </div>
                <div className="row footer-containter fixed-content-width">
                    <div className="col-sm-8 order-sm-2">
                        <p className="footer-text">{resourceManager.getResourceByKey("GL_OPERATION_PROGRAM_GOOD_MANAGEMENT_EU_L")}</p>
                    </div>
                    <div className="col-6 col-sm-2 order-sm-1 text-center text-sm-left">
                        {getFooterLogoByKey(getCurrentLang())}
                        <img className="" src={footerLogoSelected} alt="" />                   
                    </div>
                    <div className="col-6 col-sm-2 order-sm-3 text-center text-sm-right">
                        {getFooterLogoOpduByKey(getCurrentLang())}
                        <img className="" src={footerLogoOpduSelected} alt="" /> 
                    </div>
                </div>
            </footer>
        </div>);

    function cmsPageTitleMapper(pageCode: string) {

        let predifinedPageTitle = cmsPages.find(x => x.code?.toUpperCase() == pageCode?.toUpperCase())?.title

        switch (pageCode.toUpperCase()) {
            case "NEWS": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_NEWS_L");
            case "PRIVACY_POLICY": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_PRIVACY_POLICY_L");
            case "ACCESSIBILITY_POLICY": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_ACCESSIBILITY_POLICY_L");
            case "SECURITY_POLICY": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_SECURITY_POLICY_L");
            case "TERM_OF_USE": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_TERM_OF_USE_L");
            case "COOKIES": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_COOKIES_L");
            case "CONTACTS": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_CONTACTS_L");
            case "VIDEO_LESSONS": return predifinedPageTitle ?? resourceManager.getResourceByKey("GL_VIDEO_LESSONS_L");

            default: return pageCode;
        }
    }

    function getCurrentLang(): string {
        return JsCookies.get("currentLang");
    }

    function getFooterLogoByKey(key: string) {
        switch (key) {
            case "bg":
                footerLogoSelected = footerlogoEu;
                break;
            case "en":
                footerLogoSelected = footerlogoEn;
        }
    }

    function getFooterLogoOpduByKey(key: string) {
        switch (key) {
            case "bg":
                footerLogoOpduSelected = footerlogoOpdu;
                break;
            case "en":
                footerLogoOpduSelected = footerlogoOpduEn;
                break;
        }
    }
}

export default withAsyncFrame(Footer);