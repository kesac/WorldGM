import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { WorldInfo } from './components/WorldInfo';
import { Athletes } from './components/Athletes';
import { Athlete } from './components/Athlete';
import { TeamList } from './components/TeamList'

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata' component={ FetchData } />
    <Route path='/worldinfo' component={ WorldInfo } />
    <Route path='/athletes' component={ Athletes } />
    <Route path='/athlete/:id' component={ Athlete } />
    <Route path='/teams' component={ TeamList } />
</Layout>;
