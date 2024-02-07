import { UIHelper } from 'cnsys-core';
import { BaseProps } from 'cnsys-ui-react';
import { Constants, resourceManager, UserStatuses, UserVM } from 'eau-core';
import moment from 'moment';
import { value } from 'numeral';
import React from 'react';
import { Link } from 'react-router-dom';

interface UserProfileResultsProps extends BaseProps {
    userProfiles: UserVM[];
    previewMode?: boolean;
    onGetUser?: (user) => void;
}

const UserProfileResultsUI: React.FC<UserProfileResultsProps> = ({ userProfiles, previewMode, onGetUser }) => {

    const onSelectUser = (user) => {
        onGetUser(user);
    }
    
    return <div className="table-responsive">
        <table className="table table-bordered table-striped table-hover">
            <thead>
                <tr>
                    {previewMode && <th>{resourceManager.getResourceByKey("GL_CHOICE_L")}</th>}
                    <th>{resourceManager.getResourceByKey("GL_DATE_LAST_UPDATE_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_CIN_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_USERNAME_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_EMAIL_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_STATUS_L")}</th>
                    {!previewMode && <th>{resourceManager.getResourceByKey("GL_ACTIONS_L")}</th>}
                </tr>
            </thead>
            <tbody id="content">
                {
                    userProfiles.map((user, index) => {
                        return <tr key={user.userID}>
                            {previewMode && <td><input id="gender_male" type="radio"  name="gender" value="male" onChange={() => onSelectUser(user)}/></td>}
                            <td>{UIHelper.dateDisplayFor(moment(user.updatedOn), Constants.DATE_FORMATS.dateTime)}</td>
                            <td>{user.cin}</td>
                            <td>{user.username}</td>
                            <td>{user.email}</td>
                            <td className="icons-td">{renderUserStatus(user.status)}</td>
                            {!previewMode && <td className="buttons-td">
                                <Link to={`/users/profiles/edit/${user.userID}`} className="btn btn-secondary">
                                    <i className="ui-icon ui-icon-edit" title={resourceManager.getResourceByKey("GL_EDIT_L")}></i>
                                </Link>
                            </td>}
                        </tr>
                    })
                }
            </tbody>
        </table>
    </div>

    function renderUserStatus(userStatus: UserStatuses) {

       switch (Number(userStatus)) {
           case UserStatuses.Active:
                return <><i className="ui-icon ui-icon-state-active" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_ACTIVE_L")}</>

           case UserStatuses.Inactive:
                return <><i className="ui-icon ui-icon-state-inactive" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_INACTIVE_L")}</>

            case UserStatuses.Locked:
                return <><i className="ui-icon ui-icon-state-inactive" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_LOCKED_L")}</>
            
            case UserStatuses.Deactivated:
                return <><i className="ui-icon ui-icon-state-inactive" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_DEACTIVATED_L")}</>

            case UserStatuses.NotConfirmed:
                return <><i className="ui-icon ui-icon-state-inactive" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_NOT_CONFIRMED_L")}</>
       }
    }
}

export default UserProfileResultsUI;