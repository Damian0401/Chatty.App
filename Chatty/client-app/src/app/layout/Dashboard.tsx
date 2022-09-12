import { observer } from "mobx-react-lite"
import { Outlet } from "react-router-dom"
import ChatSidebar from "../../features/chat/ChatSidebar"
import Navbar from "./Navbar"



export default observer(function Dashboard() {

    return (
        <>
            <Navbar />
            <ChatSidebar />
            <Outlet />
        </>
    )
})