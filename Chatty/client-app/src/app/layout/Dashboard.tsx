import { observer } from "mobx-react-lite"
import { Outlet } from "react-router-dom"
import Navbar from "./Navbar"
import Sidebar from "./Sidebar"



export default observer(function Dashboard() {

    return (
        <>
            <Navbar />
            <Sidebar />
            <Outlet />
        </>
    )
})