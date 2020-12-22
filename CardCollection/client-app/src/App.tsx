import React, { Fragment } from 'react';
import logo from './logo.svg';
import './App.css';
import { Route } from 'react-router-dom';
import CardChecklist from './components/CardChecklist/CardChecklist';

const App = () => {
    return (
        <Fragment>
            <Route exact path='/' component={CardChecklist} />
        </Fragment>
    );
}

export default App;

