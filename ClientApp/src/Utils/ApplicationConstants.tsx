export const APP_CONSTANTS = {
    status_success: "success",
    status_error: "error",
    base_url: process.env.REACT_API_BASE_URL,
    save_url: process.env.REACT_API_BASE_URL + '/api/board/save',
    resume_url: process.env.REACT_API_BASE_URL + '/api/board/resume',
    initialize_url: process.env.REACT_API_BASE_URL + '/api/board/initialize',
    cell_check_url: process.env.REACT_API_BASE_URL + '/api/cell/check',
    cell_flag_url: process.env.REACT_API_BASE_URL + '/api/cell/flag',
    user_register_url: process.env.REACT_API_BASE_URL + '/api/user/register',
    user_getboards_url: process.env.REACT_API_BASE_URL + '/api/user/getBoards',
    user_getuser_url: process.env.REACT_API_BASE_URL + '/api/user/getuser/'
};

export default APP_CONSTANTS;