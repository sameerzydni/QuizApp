import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import Authenticate from './Component/Authenticate';
import Layout from './Component/Layout';
import Login from './Component/Login';
import Quiz from './Component/Quiz';
import Result from './Component/Result';

function App() {
  return (
    <BrowserRouter>
      <Routes>
          <Route path='/' element={<Login />}></Route>
          <Route element={<Authenticate />}>
            <Route path="/" element={<Layout></Layout>}>
            <Route path='/quiz' element={<Quiz />}></Route>
            <Route path='/result' element={<Result />}></Route>
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
