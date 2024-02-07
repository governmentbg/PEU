import { EAUBaseDataService, UserAuthentication, UserAuthenticationInfo } from 'eau-core';
import { CompleteForgottenPasswordModel, UserConfirmRegistrationResult, UserInputModel, UserRegistrationResult } from '../models/ModelsManualAdded';

export class UserService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "Users";
    }

    //#region GET

    public completeRegistration(guid: string): Promise<UserConfirmRegistrationResult> {
        return this.get<UserConfirmRegistrationResult>(`/CompleteRegistration/${guid}`, UserConfirmRegistrationResult, null);
    }

    public cancelRegistration(guid: string): Promise<string> {
        return this.get<string>(`/CancelRegistration/${guid}`, "string", null);
    }

    public getUsersAuthTypes(): Promise<UserAuthentication[]> {
        return this.get<UserAuthentication[]>(`/UsersAuthTypes`, UserAuthentication);
    }


    public getCertificates(): Promise<UserAuthenticationInfo[]> {
        return this.get<UserAuthenticationInfo[]>(`/Certificates`, UserAuthenticationInfo);
    }

    public getEAuths(): Promise<UserAuthenticationInfo[]> {
        return this.get<UserAuthenticationInfo[]>(`/EAuthentications`, UserAuthenticationInfo);
    }

    public getUserProcess(processGuid: string): Promise<boolean> {
        return this.get<boolean>(`${processGuid}/IsActiveLink`, "boolean");
    }

    public getRegistrationData(): Promise<any> {
        return this.get<any>(`/registrationData`, "any");
    }

    //#endregion

    //#region POST

    public register(model: UserInputModel): Promise<UserRegistrationResult> {
        return this.post<UserRegistrationResult>("/Register", UserRegistrationResult, model);
    }

    public resetPassword(email: string): Promise<string> {
        return this.post<string>(`/ResetPassword`, "string", email);
    }

    public renewResetPassword(processId: string): Promise<string> {
        return this.post<string>(`/RenewResetPassword`, "string", processId);
    }

    public resendConfirmationEmail(email: string): Promise<void> {
        return this.post<void>(`/ResendConfirmationEmail`, "string", email);
    }

    public renewUserRegistrationLink(processId: string): Promise<void> {
        return this.post<void>(`/RenewUserRegistration`, "string", processId)
    }

    //#endregion

    //#region PUT

    public changePassword(model: UserInputModel): Promise<UserInputModel> {
        return this.put<UserInputModel>("/ChangePassword", UserInputModel, model);
    }

    public updateUserProfile(email: string): Promise<void> {
        return this.put<void>(`/UpdateProfile`, "string", email);
    }

    public deactivateUserProfile(): Promise<void> {
        return this.put<void>(`/DeactivateUserProfile`, "string", null);
    }

    public completeForgottenPassword(model: CompleteForgottenPasswordModel): Promise<void> {
        return this.put(`/CompleteForgottenPassword`, CompleteForgottenPasswordModel, model);
    }

    //#endregion

    //#region DELETE

    public deleteUserAuthentication(userAuthenticationId: number): Promise<void> {
        return this.delete(`/DeleteUserAuthentication/${userAuthenticationId}`, null);
    }

    //#endregion
}